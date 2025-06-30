#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <stdbool.h>
#include "../include/participantes.h"
#include "../utils/utils.h"

#define MAX_PARTICIPANTES 100

/**
 * @brief Lê os participantes do arquivo e armazena em um vetor
 *
 * @param participantes Vetor de participantes para armazenar os dados
 * @param max_participantes Numero maximo de participantes que o vetor pode armazenar
 * @return int Retorna o numero de participantes lidos ou -1 em caso de erro ao abrir o arquivo
 */
int carregar_participantes(Participante *participantes, int max_participantes)
{
    FILE *fp = fopen("../../data/participantes.txt", "r");
    if (fp == NULL)
    {
        // Se o arquivo nao existir na primeira execucao, nao e um erro, apenas nao ha participantes
        // printf("Erro ao abrir o arquivo de participantes.\n\n");
        return 0; // Retorna 0 participantes se o arquivo nao existir ou houver erro
    }

    int count = 0;
    // O fscanf nao precisa de espaco antes do %49s, o %s ja ignora espacos em branco antes
    while (fscanf(fp, "%49s %49s %11s %14s %d |", participantes[count].nome, participantes[count].sobrenome,
        participantes[count].cpf, participantes[count].celular, &participantes[count].idade) == 5)
    {
        count++;
        if (count >= max_participantes)
        {
            printf("Aviso: Numero maximo de participantes (%d) atingido. Alguns participantes podem nao ser carregados.\n\n", max_participantes);
            break;
        }
    }

    fclose(fp);
    return count;
}

/**
 * @brief Salva um vetor de participantes no arquivo, sobrescrevendo o conteudo existente.
 *
 * Recebe um vetor de participantes e seu tamanho, e salva todos no arquivo.
 * Este e um helper para escrever a lista completa de volta.
 *
 * @param participantes Vetor de participantes a serem salvos
 * @param count O numero de participantes no vetor
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int salvar_participantes(Participante *participantes, int count)
{
    FILE *fp;

    // Abre o arquivo no modo de escrita "w" para sobrescrever o conteudo existente
    fp = fopen("../../data/participantes.txt", "w");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de participantes para escrita.\n\n");
        return 1;
    }

    for (int i = 0; i < count; i++)
    {
        fprintf(fp, "%s %s %s %s %d |\n", participantes[i].nome, participantes[i].sobrenome, participantes[i].cpf,
                participantes[i].celular, participantes[i].idade);
    }

    fclose(fp);
    return 0;
}

/**
 * @brief Cadastra um novo participante no sistema
 *
 * Solicita ao usuario informacoes sobre o novo participante, valida cada entrada e salva no arquivo.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int participantes_create()
{
    Participante participantes[MAX_PARTICIPANTES];
    Participante novo_participante; // Variavel para o novo participante
    int count;

    // Carrega os participantes existentes.
    // count tera o numero atual de participantes.
    count = carregar_participantes(participantes, MAX_PARTICIPANTES);
    if (count == -1) // Erro real ao carregar
    {
        return 1;
    }
    // Se count == 0, o arquivo estava vazio ou nao existia, o que e ok.

    // Verifica se ha espaco no vetor para o novo participante
    if (count >= MAX_PARTICIPANTES)
    {
        printf("Erro: Limite maximo de participantes atingido. Nao e possivel cadastrar mais.\n\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("| Cadastro de participantes |\n");
    printf("------------------------------\n");

    // Solicita e valida o nome do participante
    bool nome_valido;
    do
    {
        printf("Digite o nome: ");
        scanf(" %49[^\n]", novo_participante.nome); // Le para o novo_participante

        nome_valido = !validar_tamanho_string(novo_participante.nome, 3, 49);
        if (!nome_valido)
        {
            printf("Nome invalido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!nome_valido);

    pretty_format_name(novo_participante.nome, strlen(novo_participante.nome));

    // Solicita e valida o sobrenome do participante
    bool sobrenome_valido;
    do
    {
        printf("Digite o sobrenome do participante: ");
        scanf(" %49[^\n]", novo_participante.sobrenome); // Le para o novo_participante

        sobrenome_valido = !validar_tamanho_string(novo_participante.sobrenome, 3, 49);
        if (!sobrenome_valido)
        {
            printf("Sobrenome invalido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!sobrenome_valido);

    pretty_format_name(novo_participante.sobrenome, strlen(novo_participante.sobrenome));

    // Solicita e valida o CPF do participante
    bool cpf_valido;
    do
    {
        printf("Digite o CPF do participante (11 numeros): ");
        scanf("%11s", novo_participante.cpf); // Le para o novo_participante

        // Valida o CPF formatado (se sua funcao validate_cpf espera formatado)
        // E verifica se ja existe um participante com esse CPF
        cpf_valido = validate_cpf(novo_participante.cpf, strlen(novo_participante.cpf)) == 1;

        if (!cpf_valido)
        {
            printf("CPF invalido ou ja cadastrado! Digite outro CPF.\n\n");
        }
    } while (!cpf_valido);

    // Solicita e valida o celular do participante
    bool celular_valido;
    do
    {
        printf("Digite o contato (celular ou telefone fixo): ");
        scanf("%14s", novo_participante.celular); // Le para o novo_participante

        int tamanho_celular = strlen(novo_participante.celular);
        celular_valido = tamanho_celular >= 10 && is_valid_number(novo_participante.celular, tamanho_celular);

        if (!celular_valido)
        {
            printf("Contato invalido! Deve conter 10 ou mais numeros.\n\n");
        }
    } while (!celular_valido);

    // Solicita e valida a idade do participante
    bool idade_valida;
    do
    {
        printf("Digite a idade do participante: ");
        scanf("%d", &novo_participante.idade); // Le para o novo_participante

        idade_valida = novo_participante.idade >= 0 && novo_participante.idade <= 120;
        if (!idade_valida)
        {
            printf("Idade invalida! Deve ser um numero entre 0 e 120.\n\n");
        }
    } while (!idade_valida);

    // Adiciona o novo participante ao vetor de participantes existentes
    participantes[count] = novo_participante;
    count++; // Incrementa o contador para refletir o novo tamanho

    // Salva o vetor completo (antigos + novo) no arquivo, SOBRESCREVENDO-O
    int salvou_com_sucesso = salvar_participantes(participantes, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar o participante no arquivo.\n\n");
        return 1;
    }

    printf("Participante cadastrado com sucesso!\n\n");
    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Lista todos os participantes cadastrados
 *
 * Le os participantes do arquivo e exibe diretamente no console.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int participantes_list()
{
    Participante participantes[MAX_PARTICIPANTES]; // Vetor para armazenar ate 100 participantes
    int count = 0;                                 // Contador para o numero de participantes

    // Abre o arquivo para leitura
    count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1) // Erro real na abertura do arquivo
    {
        return 1;
    }
    else if (count == 0)
    { // Nenhum participante para listar
        printf("Nenhum participante cadastrado.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    // Exibe o cabecalho da tabela
    printf("| Listagem de participantes |\n");
    printf("------------------------------\n");
    printf("+--------------------------------------------------------------------------------------+\n");
    printf("| %-20s | %-20s | %-11s | %-14s | %-5s |\n", "Nome", "Sobrenome", "CPF", "Celular", "Idade");
    printf("+--------------------------------------------------------------------------------------+\n");

    // Exibe os participantes armazenados no vetor
    for (int i = 0; i < count; i++)
    {
        printf("| %-20s | %-20s | %-11s | %-14s | %-5d |\n", participantes[i].nome, participantes[i].sobrenome,
            participantes[i].cpf, participantes[i].celular, participantes[i].idade);
    }
    printf("+--------------------------------------------------------------------------------------+\n");

    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Atualiza os dados de um participante existente no sistema
 *
 * Solicita o CPF do participante, permite ao usuario atualizar seus campos e salva as alteracoes no arquivo.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ou se o participante nao for encontrado.
 */
int participantes_update()
{
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1)
    {
        return 1; // Erro ao abrir o arquivo
    }
    else if (count == 0)
    {
        printf("Nenhum participante cadastrado para atualizar.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    char cpf_alvo[12];
    int indice_encontrado = -1;

    // Variaveis temporarias para novas entradas
    char novo_nome[50];
    char novo_sobrenome[50];
    char novo_celular[15];
    int nova_idade;

    printf("| Atualizacao de participantes |\n");
    printf("--------------------------------\n");

    // Solicita o CPF do participante a ser atualizado
    bool cpf_valido = false;
    do
    {
        printf("Digite o CPF que deseja atualizar: ");
        scanf("%11s", cpf_alvo);

        // Procura o participante no vetor
        for (int i = 0; i < count; i++)
        {
            if (strcmp(cpf_alvo, participantes[i].cpf) == 0)
            {
                indice_encontrado = i;
                cpf_valido = true;
                break;
            }
        }

        if (indice_encontrado == -1)
        {
            printf("Participante com CPF %s nao encontrado.\n\n", cpf_alvo);
        }
    } while (!cpf_valido);

    // Atualiza os dados do participante encontrado
    printf("Atualizando dados do participante '%s %s'.\n\n", participantes[indice_encontrado].nome, participantes[indice_encontrado].sobrenome);

    // Atualiza o nome
    bool nome_valido;
    do
    {
        printf("Novo nome (atual: %s): ", participantes[indice_encontrado].nome);
        scanf(" %49[^\n]", novo_nome); // Le para a variavel temporaria

        nome_valido = !validar_tamanho_string(novo_nome, 3, 49);
        if (!nome_valido)
        {
            printf("Nome invalido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!nome_valido);
    strcpy(participantes[indice_encontrado].nome, novo_nome); // Copia apos validacao
    pretty_format_name(participantes[indice_encontrado].nome, strlen(participantes[indice_encontrado].nome));

    // Atualiza o sobrenome
    bool sobrenome_valido;
    do
    {
        printf("Novo sobrenome (atual: %s): ", participantes[indice_encontrado].sobrenome);
        scanf(" %49[^\n]", novo_sobrenome); // Le para a variavel temporaria

        sobrenome_valido = !validar_tamanho_string(novo_sobrenome, 3, 49);
        if (!sobrenome_valido)
        {
            printf("Sobrenome invalido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!sobrenome_valido);
    strcpy(participantes[indice_encontrado].sobrenome, novo_sobrenome); // Copia apos validacao
    pretty_format_name(participantes[indice_encontrado].sobrenome, strlen(participantes[indice_encontrado].sobrenome));

    // Atualiza o celular
    bool celular_valido;
    do
    {
        printf("Novo celular (atual: %s): ", participantes[indice_encontrado].celular);
        scanf("%14s", novo_celular); // Le para a variavel temporaria

        celular_valido = strlen(novo_celular) >= 10 && is_valid_number(novo_celular, strlen(novo_celular));
        if (!celular_valido)
        {
            printf("Celular invalido! Deve conter 10 ou mais numeros.\n\n");
        }
    } while (!celular_valido);
    strcpy(participantes[indice_encontrado].celular, novo_celular); // Copia apos validacao

    // Atualiza a idade
    bool idade_valida;
    do
    {
        printf("Nova idade (atual: %d): ", participantes[indice_encontrado].idade);
        scanf("%d", &nova_idade); // Le para a variavel temporaria

        idade_valida = nova_idade >= 0 && nova_idade <= 120;
        if (!idade_valida)
        {
            printf("Idade invalida! Deve ser um numero entre 0 e 120.\n\n");
        }
    } while (!idade_valida);
    participantes[indice_encontrado].idade = nova_idade; // Copia apos validacao

    // Sobrescreve o arquivo com os dados atualizados
    int salvou_com_sucesso = salvar_participantes(participantes, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar as alteracoes no arquivo.\n\n");
        return 1;
    }

    printf("Dados do participante atualizados com sucesso!\n\n");
    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Apaga um participante especifico pelo CPF
 *
 * Solicita o CPF do participante a ser removido e o exclui do arquivo.
 * Utiliza um vetor para a operacao de exclusao.
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_delete()
{
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1)
    {
        return 1; // Erro ao abrir o arquivo
    }
    else if (count == 0)
    {
        printf("Nenhum participante cadastrado para apagar.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    char cpf_alvo[12];
    int indice_encontrado = -1;

    printf("| Remocao de participante |\n");
    printf("--------------------------------\n");

    // Solicita o CPF do participante a ser removido
    bool cpf_valido;
    do
    {
        printf("Digite o CPF do participante que deseja apagar: ");
        scanf("%11s", cpf_alvo);

        // A validacao aqui e se o CPF existe na lista de participantes
        cpf_valido = participante_existe(cpf_alvo) == 1; // Verifica se o CPF existe
        if (!cpf_valido)
        {
            printf("Participante com CPF %s nao encontrado. Digite um CPF existente.\n\n", cpf_alvo);
        }
    } while (!cpf_valido);

    // Procura o participante no vetor
    for (int i = 0; i < count; i++)
    {
        if (strcmp(participantes[i].cpf, cpf_alvo) == 0)
        {
            indice_encontrado = i;
            break;
        }
    }

    // Nao precisa verificar se indice_encontrado == -1 novamente, pois participante_existe ja garante que ele existe
    // if (indice_encontrado == -1) { /* este bloco nao sera mais alcancado com a nova validacao */ }

    // Remove o participante do vetor deslocando os elementos seguintes
    for (int i = indice_encontrado; i < count - 1; i++)
    {
        participantes[i] = participantes[i + 1];
    }
    count--; // Reduz o numero de participantes

    // Sobrescreve o arquivo com os dados atualizados (sem o participante removido)
    int salvou_com_sucesso = salvar_participantes(participantes, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao remover participante no arquivo.\n\n");
        return 1;
    }

    printf("Participante com CPF %s removido com sucesso!\n\n", cpf_alvo);
    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Verifica se um participante existe pelo CPF
 *
 * @param cpf O CPF do participante a ser verificado
 * @return int 1 se o participante existir, 0 caso contrario
 */
int participante_existe(const char *cpf)
{
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1)
    {
        return 0; // Erro ao abrir o arquivo, assumimos que nao existe
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(participantes[i].cpf, cpf) == 0)
        {
            return 1; // Participante encontrado
        }
    }

    return 0; // Participante nao encontrado
}