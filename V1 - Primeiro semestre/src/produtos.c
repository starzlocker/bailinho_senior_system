#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "../include/produtos.h"
#include "../utils/utils.h"

// Capacidade maxima para o vetor de produtos.
#define MAX_PRODUTOS 100

/**
 * @brief Le os produtos do arquivo e armazena em um vetor
 *
 * @param produtos Vetor de produtos para armazenar os dados
 * @param max_produtos Numero maximo de produtos que o vetor pode armazenar
 * @return int Retorna o numero de produtos lidos ou -1 em caso de erro ao abrir o arquivo
 */
int carregar_produtos(Produto *produtos, int max_produtos)
{
    FILE *fp = fopen("../../data/produtos.txt", "r"); // Caminho ajustado
    if (fp == NULL)
    {
        // Se o arquivo nao existir na primeira execucao, nao e um erro, apenas nao ha produtos
        return -1; // Retorna 0 produtos se o arquivo nao existir ou houver erro
    }

    int count = 0;
    while (fscanf(fp, "%49s %f %d |", produtos[count].nome, &produtos[count].valor, &produtos[count].quantidade) == 3)
    {
        count++;
        if (count >= max_produtos)
        {
            printf("Aviso: Numero maximo de produtos (%d) atingido. Alguns produtos podem nao ser carregados.\n\n", max_produtos);
            break;
        }
    }

    fclose(fp);
    return count;
}

/**
 * @brief Salva um vetor de produtos no arquivo, sobrescrevendo o conteudo existente.
 *
 * Recebe um vetor de produtos e seu tamanho, e salva todos no arquivo.
 * Este e um helper para escrever a lista completa de volta.
 *
 * @param produtos Vetor de produtos a serem salvos
 * @param count O numero de produtos no vetor
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int salvar_produtos(Produto *produtos, int count)
{
    FILE *fp;

    // Abre o arquivo no modo de escrita "w" para sobrescrever o conteudo existente
    fp = fopen("../../data/produtos.txt", "w"); // Caminho ajustado
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de produtos para escrita.\n\n");
        return 1;
    }

    for (int i = 0; i < count; i++)
    {
        fprintf(fp, "%s %.2f %d |\n", produtos[i].nome, produtos[i].valor, produtos[i].quantidade);
    }

    fclose(fp);
    return 0;
}

/**
 * @brief Cadastra um novo produto no sistema
 *
 * Solicita ao usuario informacoes sobre o novo produto e
 * armazena as informacoes do produto em um arquivo de texto (produtos.txt).
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_create()
{
    Produto produtos[MAX_PRODUTOS]; // Vetor para carregar produtos existentes
    Produto novo_produto;           // Variavel para o novo produto
    int count;

    // Carrega os produtos existentes
    count = carregar_produtos(produtos, MAX_PRODUTOS);
    if (count == -1) // Erro real ao carregar
    {
        return 1;
    }
    // Se count == 0, o arquivo estava vazio ou nao existia, o que e ok.

    // Verifica se ha espaco no vetor para o novo produto
    if (count >= MAX_PRODUTOS)
    {
        printf("Erro: Limite maximo de produtos atingido. Nao e possivel cadastrar mais.\n\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("| Cadastro de produtos |\n");
    printf("------------------------------\n");

    // Solicita e valida o nome do produto
    bool nome_invalido, contem_espacos;
    do
    {
        printf("Digite o nome (ex: coca_cola): "); 
        scanf(" %49[^\n]", novo_produto.nome);

        // Primeiro, verifica se o nome ja existe
        if (produto_existe(novo_produto.nome))
        {
            printf("Nome do produto ja existe! Digite outro nome.\n\n");
            nome_invalido = true; // Forca a repeticao do loop
            continue;             // Pula para a proxima iteracao do do-while
        }

        nome_invalido = validar_tamanho_string(novo_produto.nome, 1, 49);

        if (nome_invalido)
        {
            printf("Nome invalido! Deve ter entre 1 e 50 caracteres.\n\n"); 
            continue;
        }

        contem_espacos = contem_espaco(novo_produto.nome);

        if (contem_espacos)
        {
            printf("Nome invalido! Nao deve conter espacos.\n\n"); 
        }
    } while (nome_invalido || contem_espacos);

    // Solicita e valida o valor do produto
    bool valor_valido;
    do
    {
        printf("Digite o valor (ex: 30,45): "); 
        scanf("%f", &novo_produto.valor);

        valor_valido = novo_produto.valor >= 0; // Pode ser 0? Ajuste se necessario
        if (!valor_valido)
        {
            printf("Valor invalido! Deve ser um numero positivo ou zero.\n\n"); 
        }
    } while (!valor_valido);

    // Solicita e valida a quantidade do produto
    bool quantidade_valida;
    do
    {
        printf("Digite a quantidade do produto: "); 
        scanf("%d", &novo_produto.quantidade);

        quantidade_valida = novo_produto.quantidade >= 0;
        if (!quantidade_valida)
        {
            printf("Quantidade invalida! Deve ser um numero inteiro positivo ou zero.\n\n"); 
        }
    } while (!quantidade_valida);

    // Adiciona o novo produto ao vetor
    produtos[count] = novo_produto;
    count++; // Incrementa o contador

    // Salva o vetor completo (antigos + novo) no arquivo, SOBRESCREVENDO-O
    int salvou_com_sucesso = salvar_produtos(produtos, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar o produto no arquivo.\n\n");
        return 1;
    }

    printf("Produto cadastrado com sucesso!\n\n"); 
    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Lista todos os produtos cadastrados
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_list()
{
    Produto produtos[MAX_PRODUTOS]; // Vetor para armazenar ate MAX_PRODUTOS
    int count = 0;                  // Contador para o numero de produtos

    // Carrega os produtos existentes
    count = carregar_produtos(produtos, MAX_PRODUTOS);

    // Log: Resultado do carregamento
    if (count == -1)
    {
        printf(" Erro ao carregar produtos. Arquivo pode estar ausente ou corrompido.\n");
        return 1;
    }
    else if (count == 0)
    {
        printf("Nenhum produto cadastrado.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    // Exibe o cabecalho da tabela

    printf("|   Listagem de produtos     |\n");
    printf("------------------------------\n");
    printf("+-------------------------------------------------------------------------+\n");
    printf("| %-40s | %-15s | %-15s |\n", "Nome", "Valor", "Quantidade");
    printf("|-------------------------------------------------------------------------|\n");

    // Exibe os produtos armazenados no vetor
    for (int i = 0; i < count; i++)
    {
        printf("| %-40s | R$ %-12.2f | %-15d |\n", produtos[i].nome, produtos[i].valor, produtos[i].quantidade);
    }
    printf("+-------------------------------------------------------------------------+\n");

    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Atualiza os dados de um produto especifico
 *
 * Solicita o nome do produto a ser atualizado e, se encontrado,
 * permite a edicao de todos os seus dados. Utiliza um vetor para a operacao.
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro ou produto nao encontrado
 */
int produtos_update()
{
    Produto produtos[MAX_PRODUTOS];
    int count = 0;
    char nome_alvo[50];
    int indice_encontrado = -1; // Usado para armazenar o indice do produto encontrado

    // Variaveis temporarias para as novas entradas antes da validacao
    char novo_nome[50];
    float novo_valor;
    int nova_quantidade;

    // 1. Le todos os produtos existentes para o vetor
    count = carregar_produtos(produtos, MAX_PRODUTOS);
    if (count == -1) // Erro real ao carregar
    {
        return 1;
    }
    else if (count == 0)
    {                                                            // Nenhum produto para atualizar
        printf("Nenhum produto cadastrado para atualizar.\n\n"); 
        pausar();
        limpar_tela();
        return 0;
    }

    printf("|   Atualizacao de produtos    |\n"); 
    printf("--------------------------------\n");

    // 2. Solicita o nome do produto a ser atualizado e o procura
    bool produto_alvo_valido = false;
    do
    {
        printf("Digite o produto que deseja atualizar: ");
        scanf(" %49[^\n]", nome_alvo);

        for (int i = 0; i < count; i++)
        {
            if (strcmp(nome_alvo, produtos[i].nome) == 0)
            {
                indice_encontrado = i;
                produto_alvo_valido = true;
                break;
            }
        }

        if (!produto_alvo_valido)
        {
            printf("Produto '%s' nao encontrado. Digite um nome de produto existente.\n\n", nome_alvo); 
        }
    } while (!produto_alvo_valido);

    // 3. Se o produto foi encontrado, solicita e valida os novos dados
    printf("\n\nDigite os novos dados:\n");

    // Solicita e valida o novo nome do produto
    bool nome_valido, contem_espacos;
    do
    {
        printf("Novo nome do produto (atual: %s): ", produtos[indice_encontrado].nome); 
        scanf(" %49[^\n]", novo_nome);                                                                // Le para a variavel temporaria

        // Valida se o novo nome ja existe (e nao e o nome do produto atual sendo editado)
        if (strcmp(novo_nome, produtos[indice_encontrado].nome) != 0 && produto_existe(novo_nome))
        {
            printf("Nome do produto ja existe! Escolha outro nome.\n\n"); 
            nome_valido = false;                                          // Forca a repeticao do loop
            continue;
        }

        nome_valido = !validar_tamanho_string(novo_nome, 1, 49);
        if (!nome_valido)
        {
            printf("Nome invalido! Deve ter entre 1 e 50 caracteres.\n\n"); 
            continue;
        }

        contem_espacos = contem_espaco(novo_nome);
        if (contem_espacos)
        {
            printf("Nome invalido! Nao deve conter espacos.\n\n"); 
        }
    } while (!nome_valido || contem_espacos);
    strcpy(produtos[indice_encontrado].nome, novo_nome); // Copia apos validacao

    // Solicita e valida o novo valor do produto
    bool valor_valido;
    do
    {
        printf("Novo valor (atual: R$ %.2f): ", produtos[indice_encontrado].valor);
        scanf("%f", &novo_valor);                                                              // Le para a variavel temporaria

        valor_valido = novo_valor >= 0;
        if (!valor_valido)
        {
            printf("Valor invalido! Deve ser um numero positivo ou zero.\n\n");
        }
    } while (!valor_valido);
    produtos[indice_encontrado].valor = novo_valor; // Copia apos validacao

    // Solicita e valida a nova quantidade do produto
    bool quantidade_valida;
    do
    {
        printf("Nova quantidade no estoque (atual: %d): ", produtos[indice_encontrado].quantidade);
        scanf("%d", &nova_quantidade);                                                              // Le para a variavel temporaria

        quantidade_valida = nova_quantidade >= 0;
        if (!quantidade_valida)
        {
            printf("Quantidade invalida! Deve ser um numero inteiro positivo ou zero.\n\n");
        }
    } while (!quantidade_valida);
    produtos[indice_encontrado].quantidade = nova_quantidade; // Copia apos validacao

    // 4. Sobrescreve o arquivo original com os dados atualizados do vetor
    int salvou_com_sucesso = salvar_produtos(produtos, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar as alteracoes no arquivo.\n\n");
        return 1;
    }

    printf("Dados atualizados com sucesso!\n\n");
    limpar_tela();
    return 0;
}

/**
 * @brief Apaga um produto especifico pelo nome
 *
 * Solicita o nome do produto a ser removido e o exclui do arquivo.
 * Utiliza um vetor para a operacao de exclusao.
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_delete()
{
    Produto produtos[MAX_PRODUTOS];
    int count = carregar_produtos(produtos, MAX_PRODUTOS);

    if (count == -1)
    { // Erro real ao carregar
        return 1;
    }
    else if (count == 0)
    {                                                         // Nenhum produto para apagar
        printf("Nenhum produto cadastrado para apagar.\n\n"); 
        pausar();
        limpar_tela();
        return 0;
    }

    char nome_alvo[50];
    int indice_encontrado = -1;

    printf("|       Remocao de produto     |\n"); 
    printf("--------------------------------\n");

    // Solicita o nome do produto a ser removido e valida se ele existe
    bool produto_alvo_valido = false;
    do
    {
        printf("Digite o nome do produto que deseja apagar: ");
        scanf(" %49[^\n]", nome_alvo);

        // Procura o produto no vetor (reutiliza a logica, nao chama produto_existe diretamente pois ja carregamos)
        for (int i = 0; i < count; i++)
        {
            if (strcmp(produtos[i].nome, nome_alvo) == 0)
            {
                indice_encontrado = i;
                produto_alvo_valido = true;
                break;
            }
        }

        if (!produto_alvo_valido)
        {
            printf("Produto '%s' nao encontrado. Digite um nome de produto existente.\n\n", nome_alvo); 
        }
    } while (!produto_alvo_valido);

    // Remove o produto do vetor deslocando os elementos seguintes
    for (int i = indice_encontrado; i < count - 1; i++)
    {
        produtos[i] = produtos[i + 1];
    }
    count--; // Reduz o numero de produtos

    // Salva o vetor atualizado (sem o produto removido) no arquivo, sobrescrevendo-o
    int salvou_com_sucesso = salvar_produtos(produtos, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao remover produto no arquivo.\n\n"); 
        return 1;
    }

    printf("Produto '%s' removido com sucesso!\n\n", nome_alvo); 
    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Verifica se um produto existe pelo nome
 *
 * @param nome O nome do produto a ser verificado
 * @return int 1 se o produto existir, 0 caso contrario
 */
int produto_existe(const char *nome)
{
    Produto produtos[MAX_PRODUTOS];
    int count = carregar_produtos(produtos, MAX_PRODUTOS);

    if (count == -1)
    {
        return 0; // Erro ao carregar, assume que nao existe
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(produtos[i].nome, nome) == 0)
        {
            return 1; // Produto encontrado
        }
    }

    return 0; // Produto nao encontrado
}

/**
 * @brief Verifica se ha estoque disponivel para um produto
 *
 * @param nome O nome do produto a ser verificado
 * @return int 1 se houver estoque disponivel (quantidade > 0), 0 caso contrario
 */
int produto_estoque_disponivel(const char *nome)
{
    Produto produtos[MAX_PRODUTOS];
    int count = carregar_produtos(produtos, MAX_PRODUTOS);

    if (count == -1)
    {
        printf("Erro ao carregar produtos para verificar estoque.\n\n"); 
        return 0;
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(produtos[i].nome, nome) == 0)
        {
            printf("\nEstoque disponivel para %s: %d\n\n", produtos[i].nome, produtos[i].quantidade); // Log e acentos removidos
            return produtos[i].quantidade > 0;
        }
    }

    printf("Produto '%s' nao encontrado para verificar estoque.\n\n", nome); // Log e acentos removidos
    return 0;
}

/**
 * @brief Verifica se uma quantidade especifica de um produto esta disponivel em estoque
 *
 * @param nome O nome do produto
 * @param quantidade A quantidade a ser verificada
 * @return int 1 se a quantidade estiver disponivel, 0 caso contrario
 */
int produto_quantidade_disponivel(const char *nome, int quantidade)
{
    Produto produtos[MAX_PRODUTOS];
    int count = carregar_produtos(produtos, MAX_PRODUTOS);

    if (count == -1)
    {
        return 0; // Erro ao carregar
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(produtos[i].nome, nome) == 0)
        {
            return produtos[i].quantidade >= quantidade;
        }
    }

    return 0; // Produto nao encontrado ou quantidade insuficiente
}

/**
 * @brief Abate a quantidade de um produto no estoque apos uma venda
 *
 * @param nome O nome do produto
 * @param quantidade A quantidade a ser abatida
 * @return void
 */
void abater_estoque(const char *nome, int quantidade)
{
    Produto produtos[MAX_PRODUTOS];
    int count = carregar_produtos(produtos, MAX_PRODUTOS);

    if (count == -1)
    {
        printf("Erro ao carregar produtos para abater estoque.\n\n"); 
        return;
    }

    int indice_encontrado = -1;
    for (int i = 0; i < count; i++)
    {
        if (strcmp(produtos[i].nome, nome) == 0)
        {
            indice_encontrado = i;
            break;
        }
    }

    if (indice_encontrado != -1)
    {
        if (produtos[indice_encontrado].quantidade >= quantidade)
        {
            produtos[indice_encontrado].quantidade -= quantidade; // Abate a quantidade vendida
            salvar_produtos(produtos, count);                     // Salva a lista atualizada
            printf("Estoque de '%s' abatido em %d unidades. Novo estoque: %d.\n\n",
                nome, quantidade, produtos[indice_encontrado].quantidade); 
        }
        else
        {
            printf("Erro: Nao ha estoque suficiente para abater %d unidades de '%s'. Estoque atual: %d.\n\n",
                quantidade, nome, produtos[indice_encontrado].quantidade); 
        }
    }
    else
    {
        printf("Erro: Produto '%s' nao encontrado para abater estoque.\n\n", nome); 
    }
}