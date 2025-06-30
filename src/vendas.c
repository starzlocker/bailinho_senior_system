#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <time.h>
#include "../include/vendas.h"
#include "../include/participantes.h"
#include "../include/eventos.h"
#include "../include/produtos.h"
#include "../utils/utils.h"

// Capacidade maxima para o vetor de vendas.
#define MAX_VENDAS 100

/**
 * @brief Le as vendas do arquivo e armazena em um vetor
 *
 * @param vendas Vetor de vendas para armazenar os dados
 * @param max_vendas Numero maximo de vendas que o vetor pode armazenar
 * @return int Retorna o numero de vendas lidas ou 0 em caso de erro ao abrir o arquivo
 */
int carregar_vendas(Venda *vendas, int max_vendas)
{
    FILE *fp = fopen("../../data/vendas.txt", "r");
    if (fp == NULL)
    {
        // Se o arquivo nao existir na primeira execucao, nao e um erro, apenas nao ha vendas
        return 0; // Retorna 0 vendas se o arquivo nao existir ou houver erro
    }

    int count = 0;
    while (fscanf(fp, "%11s %49s %49s %d |", vendas[count].cpf_participante, vendas[count].nome_evento, vendas[count].nome_produto, &vendas[count].quantidade) == 4)
    {
        count++;
        if (count >= max_vendas)
        {
            printf("Aviso: Numero maximo de vendas (%d) atingido. Algumas vendas podem nao ser carregadas.\n\n", max_vendas);
            break;
        }
    }

    fclose(fp);
    return count;
}

/**
 * @brief Salva um vetor de vendas no arquivo, sobrescrevendo o conteudo existente.
 *
 * Recebe um vetor de vendas e seu tamanho, e salva todos no arquivo.
 * Este e um helper para escrever a lista completa de volta.
 *
 * @param vendas Vetor de vendas a serem salvos
 * @param count O numero de vendas no vetor
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int salvar_vendas(Venda *vendas, int count)
{
    FILE *fp;

    // Abre o arquivo no modo de escrita "w" para sobrescrever o conteudo existente
    fp = fopen("../../data/vendas.txt", "w");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de vendas para escrita.\n\n");
        return 1;
    }

    for (int i = 0; i < count; i++)
    {
        fprintf(fp, "%s %s %s %d |\n", vendas[i].cpf_participante, vendas[i].nome_evento, vendas[i].nome_produto, vendas[i].quantidade);
    }

    fclose(fp);
    return 0;
}

/**
 * @brief Realiza o cadastro de uma nova venda no sistema.
 *
 * Solicita ao usuario informacoes sobre a venda (CPF do participante, nome do evento,
 * nome e quantidade do produto), validando cada entrada.
 * Abate a quantidade vendida do estoque do produto e registra a venda no arquivo.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro.
 */
int vendas_create() {
    Venda vendas[MAX_VENDAS]; // Vetor para carregar vendas existentes
    Venda nova_venda;         // Variavel temporaria para a nova venda
    int count;

    // Carrega as vendas existentes
    count = carregar_vendas(vendas, MAX_VENDAS);
    if (count == -1) // Erro real ao carregar (mas carregar_vendas retorna 0 se arquivo nao existe)
    {
        // Se carregar_vendas retornar -1, significa que o fopen falhou mesmo com arquivo vazio/nao existente.
        // A implementacao atual de carregar_vendas retorna 0, entao -1 nao deveria ser retornado aqui.
        // Mantenho para seguranca, caso a logica de carregar_vendas mude.
        printf("Erro ao carregar vendas existentes.\n\n");
        return 1;
    }

    // Verifica se ha espaco no vetor para a nova venda
    if (count >= MAX_VENDAS) {
        printf("Erro: Limite maximo de vendas atingido. Nao e possivel cadastrar mais.\n\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("| Cadastro de vendas |\n");
    printf("------------------------------\n");

    // Variaveis temporarias para as entradas
    char temp_cpf_participante[12];
    char temp_nome_evento[50];
    char temp_nome_produto[50];
    int temp_quantidade;

    // Solicita e valida o CPF do participante
    bool participante_valido;
    do {
        printf("Digite o CPF do participante: ");
        scanf("%11s", temp_cpf_participante);

        participante_valido = participante_existe(temp_cpf_participante);
        if (!participante_valido) {
            printf("Participante com CPF %s nao encontrado! Tente novamente.\n\n", temp_cpf_participante); // Acentos removidos
        }
    } while (!participante_valido);
    strcpy(nova_venda.cpf_participante, temp_cpf_participante); // Copia apos validacao


    // Solicita e valida o nome do evento
    bool evento_valido;
    do {
        printf("Digite o nome do evento: ");
        scanf(" %49[^\n]", temp_nome_evento); // Permite espacos no nome do evento

        evento_valido = evento_existe(temp_nome_evento);
        if (!evento_valido) {
            printf("Evento %s nao encontrado! Tente novamente.\n\n", temp_nome_evento); // Acentos removidos
        } else if (evento_passado(temp_nome_evento)) {
            printf("Evento %s ja aconteceu! Nao e possivel realizar a venda.\n\n", temp_nome_evento); // Acentos removidos
            evento_valido = false; // Forca a repeticao do loop
        }
    } while (!evento_valido);
    strcpy(nova_venda.nome_evento, temp_nome_evento); // Copia apos validacao


    // Solicita e valida o nome do produto e estoque
    bool produto_encontrado_e_com_estoque;
    do {
        printf("Digite o nome do produto: ");
        scanf(" %49[^\n]", temp_nome_produto); // Permite espacos no nome do produto

        produto_encontrado_e_com_estoque = produto_existe(temp_nome_produto);
        if (!produto_encontrado_e_com_estoque) {
            printf("Produto %s nao encontrado! Tente novamente.\n\n", temp_nome_produto); // Acentos removidos
        } else {
            produto_encontrado_e_com_estoque = produto_estoque_disponivel(temp_nome_produto); // Imprime o estoque disponivel
            if (!produto_encontrado_e_com_estoque) {
                 printf("Produto %s esta sem estoque! Nao e possivel realizar a venda.\n\n", temp_nome_produto); // Acentos removidos
            }
        }
    } while (!produto_encontrado_e_com_estoque);
    strcpy(nova_venda.nome_produto, temp_nome_produto); // Copia apos validacao


    // Solicita e valida a quantidade do produto
    bool quantidade_valida;
    do {
        printf("Digite a quantidade do produto: ");
        scanf("%d", &temp_quantidade);

        quantidade_valida = temp_quantidade > 0;
        if (!quantidade_valida) {
            printf("Quantidade invalida! Deve ser um numero inteiro positivo.\n\n"); // Acentos removidos
        } else {
            quantidade_valida = produto_quantidade_disponivel(nova_venda.nome_produto, temp_quantidade);
            if (!quantidade_valida) {
                printf("Estoque insuficiente para o produto %s! Tente novamente.\n\n", nova_venda.nome_produto); // Acentos removidos
            }
        }
    } while (!quantidade_valida);
    nova_venda.quantidade = temp_quantidade; // Copia apos validacao

    // Abate a quantidade do produto no estoque (esta funcao ja salva o arquivo de produtos)
    abater_estoque(nova_venda.nome_produto, nova_venda.quantidade);

    // Adiciona a nova venda ao vetor
    vendas[count] = nova_venda;
    count++; // Incrementa o contador

    // Salva o vetor completo (antigos + novo) no arquivo, SOBRESCREVENDO-O
    int salvou_com_sucesso = salvar_vendas(vendas, count);
    if (salvou_com_sucesso != 0) {
        printf("Erro ao salvar a venda no arquivo.\n\n");
        return 1;
    }

    printf("Venda cadastrada com sucesso!\n\n");
    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Lista todas as vendas cadastradas
 *
 * Le as vendas do arquivo e exibe em formato tabular no console.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int vendas_list() {
    Venda vendas[MAX_VENDAS]; // Vetor para armazenar ate MAX_VENDAS
    int count = 0;           // Contador para o numero de vendas

    // Carrega as vendas existentes
    count = carregar_vendas(vendas, MAX_VENDAS);

    if (count == -1) { // Erro real na abertura do arquivo (mas carregar_vendas retorna 0)
        return 1;
    } else if (count == 0) { // Nenhuma venda para listar
        printf("Nenhuma venda cadastrada.\n\n"); // Acentos removidos
        pausar();
        limpar_tela();
        return 0;
    }


    printf("|     Listagem de vendas     |\n");
    printf("------------------------------\n");
    printf("+------------------+---------------------+---------------------+------------+\n"); // Caracteres ASCII
    printf("| %-16s | %-19s | %-19s | %-10s |\n", "CPF Participante", "Evento", "Produto", "Quantidade"); // Acentos removidos
    printf("|------------------+---------------------+---------------------+------------|\n"); // Caracteres ASCII

    for (int i = 0; i < count; i++) {
        printf("| %-16s | %-19s | %-19s | %-10d |\n", vendas[i].cpf_participante, vendas[i].nome_evento, vendas[i].nome_produto, vendas[i].quantidade);
    }
    printf("+------------------+---------------------+---------------------+------------+\n"); // Caracteres ASCII

    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Apaga uma venda especifica pelo CPF do participante e nome do evento
 *
 * Solicita o CPF do participante e o nome do evento da venda a ser removida,
 * e a exclui do vetor e depois sobrescreve o arquivo.
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro ou venda nao encontrada
 */
int vendas_delete() {
    Venda vendas[MAX_VENDAS];
    int count = carregar_vendas(vendas, MAX_VENDAS);

    if (count == -1) { // Erro real ao carregar
        return 1;
    } else if (count == 0) { // Nenhuma venda para apagar
        printf("Nenhuma venda cadastrada para apagar.\n\n"); // Acentos removidos
        pausar();
        limpar_tela();
        return 0;
    }

    char cpf_alvo[12];
    char evento_alvo[50];
    int indice_encontrado = -1;

    printf("| Remocao de venda |\n"); // Acentos removidos
    printf("--------------------------------\n");

    // Solicita o CPF e o Evento para identificar a venda
    bool venda_encontrada_valida = false;
    do {
        printf("Digite o CPF do participante da venda que deseja apagar: ");
        scanf("%11s", cpf_alvo);

        printf("Digite o nome do evento da venda que deseja apagar: ");
        scanf(" %49[^\n]", evento_alvo); // Permite espacos

        // Procura a venda no vetor
        for (int i = 0; i < count; i++) {
            if (strcmp(vendas[i].cpf_participante, cpf_alvo) == 0 &&
                strcmp(vendas[i].nome_evento, evento_alvo) == 0) {
                indice_encontrado = i;
                venda_encontrada_valida = true;
                break;
            }
        }

        if (!venda_encontrada_valida) {
            printf("Venda com CPF '%s' e Evento '%s' nao encontrada! Tente novamente.\n\n", cpf_alvo, evento_alvo); // Acentos removidos
        }
    } while (!venda_encontrada_valida);


    // Remove a venda do vetor deslocando os elementos seguintes
    for (int i = indice_encontrado; i < count - 1; i++) {
        vendas[i] = vendas[i + 1];
    }
    count--; // Reduz o numero de vendas

    // Salva o vetor atualizado (sem a venda removida) no arquivo, sobrescrevendo-o
    int salvou_com_sucesso = salvar_vendas(vendas, count);
    if (salvou_com_sucesso != 0) {
        printf("Erro ao remover venda no arquivo.\n\n"); // Acentos removidos
        return 1;
    }

    printf("Venda para o CPF '%s' no evento '%s' removida com sucesso!\n\n", cpf_alvo, evento_alvo); // Acentos removidos
    pausar();
    limpar_tela();

    return 0;
}