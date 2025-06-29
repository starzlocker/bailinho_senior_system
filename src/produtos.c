#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../include/produtos.h"
#include "../utils/utils.h"

int valida; // Variável global para validação de entradas

/**
 * @brief Cadastra um novo produto no sistema
 * 
 * Solicita ao usuário informações sobre o novo produto e
 * armazena as informacoes do produto em um arquivo de texto (produtos.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_create() {
    Produto produto;
    FILE *fp;

    fp = fopen("data/produtos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    do {
        printf("Digite o nome do produto (sem espaço): "); 
        scanf(" %49[^\n]", produto.nome);

        valida = validar_tamanho_string(produto.nome, 1, 49);

        if (valida) {
            printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
            continue;
        }

        valida = contem_espaco(produto.nome);

        if (valida) {
            printf("Nome inválido! Não deve conter espaços.\n");
        }
    } while (valida == 1);

    do {
        valida = 0;
        printf("Digite o valor do produto: ");
        scanf("%f", &produto.valor);

        if (produto.valor < 0) {
            printf("Valor inválido! Valor do produto deve ser um número positivo.\n");
            valida = 1;
        }
    } while (valida == 1);

    do {
        valida = 0;
        printf("Digite a quantidade do produto: ");
        scanf("%d", &produto.quantidade);

        if (produto.quantidade < 0) {
            printf("Quantidade inválida! Deve ser um número inteiro positivo.\n");
            valida = 1;
        }
    } while (valida == 1);

    fprintf(fp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);
    
    fclose(fp);
    limpar_tela();
    
    return 0;
}


/** 
 * @brief Lista todos os produtos cadastrados 
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_list() {
    Produto produto;
    FILE *fp;

    fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }


    printf("\nLista de produtos:\n");
    printf("╔══════════════════════════════════════════════════════╗\n");
    printf("║ %-40s ║ %-15s ║ %-15s ║\n", "Nome", "Valor", "Quantidade");
    printf("║------------------------------------------------------║\n");
    
    while (fscanf(fp, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        printf("║ %-40s ║ R$ %-12.2f ║ %-15d ║\n", produto.nome, produto.valor, produto.quantidade);
    }
    printf("╚══════════════════════════════════════════════════════╝\n");

    fclose(fp);
    pausar();
    limpar_tela();
    
    return 0;
}


/**
 * @brief Atualiza os dados de um produto específico  
 * 
 * Solicita o nome do produto a ser atualizado e, se encontrado,
 * permite a edição de todos os seus dados. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int produtos_update() {
    FILE *fp_original, *fp_temp;
    Produto produto, new_produto;
    char nome_alvo[50];
    int encontrado = 0;

    printf("Digite o nome do produto que deseja atualizar: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/produtos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada produto do arquivo original
    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(nome_alvo, produto.nome) != 0) {
            fprintf(fp_temp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);

        } else {
            encontrado = 1;

            do {
                printf("Digite o novo nome do produto (sem espaços - atual: %s): ", produto.nome);
                scanf(" %49[^\n]", new_produto.nome);

                valida = validar_tamanho_string(new_produto.nome, 1, 49);

                if (valida) {
                    printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
                    continue;
                }

                valida = contem_espaco(new_produto.nome);

                if (valida) {
                    printf("Nome inválido! Não deve conter espaços.\n");
                }
            } while (valida == 1);

            do {
                valida = 0;
                printf("Digite o novo valor do produto (atual: R$ %.2f): ", produto.valor);
                scanf("%f", &new_produto.valor);

                if (new_produto.valor < 0) {
                    printf("Valor inválido! Valor do produto deve ser um número positivo.\n");
                    valida = 1;
                }
            } while (valida == 1);

            do {
                valida = 0;
                printf("Digite a nova quantidade do produto (atual: %d): ", produto.quantidade);
                scanf("%d", &new_produto.quantidade);

                if (new_produto.quantidade < 0) {
                    printf("Quantidade inválida! Deve ser um número inteiro positivo.\n");
                    valida = 1;
                }
            } while (valida == 1);

            fprintf(fp_temp, "%s %f %d |\n", new_produto.nome, new_produto.valor, new_produto.quantidade);
            
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/produtos.txt");
    rename("data/temp.txt", "data/produtos.txt");

    if (encontrado == 0) {
        printf("Produto não encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
    limpar_tela();
    return 0;
}


/**
 * @brief Apaga um produto específico pelo nome
 * 
 * Solicita o nome do produto a ser removido e o exclui do arquivo.
 * Utiliza um arquivo temporário para a operação de exclusão.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_delete() {
    FILE *fp_original, *fp_temp;
    Produto produto;
    char nome_alvo[50];
    int encontrado = 0;

    printf("Digite o nome do produto que deseja apagar: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/produtos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada produto do arquivo original
    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
        if (strcmp(produto.nome, nome_alvo) != 0) {
            fprintf(fp_temp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);
        } else {
            encontrado = 1;
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Exclui o arquivo original
    remove("data/produtos.txt");
    // Renomeia o arquivo temporário para o nome do arquivo original
    rename("data/temp.txt", "data/produtos.txt");

    if (encontrado) {
        printf("Produto %s removido com sucesso!\n", nome_alvo);
    } else {
        printf("\n⚠️ Produto %s não encontrado! ⚠️\n", nome_alvo);
    }

    pausar();
    limpar_tela();
    
    return 0;
}

int produto_existe(const char *nome) {
    Produto produto;
    char linha[256]; // Buffer para armazenar cada linha do arquivo
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro: Não foi possível abrir o arquivo de produtos.\n");
        return 0;
    }

    while (fgets(linha, sizeof(linha), fp)) {

        // Substitui vírgulas por pontos na linha lida
        for (char *p = linha; *p; p++) {
            if (*p == ',') {
                *p = '.';
            }
        }

        int result = sscanf(linha, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade);
        if (result == 3) {
            if (strcmp(produto.nome, nome) == 0) {
                fclose(fp);
                return 1;
            }
        }
    }

    fclose(fp);
    return 0;
}

int produto_estoque_disponivel(const char *nome) {
    Produto produto;
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de produtos.\n");
        return 0;
    }

    while (fscanf(fp, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            fclose(fp);
            printf("\nEstoque disponível para %s: %d\n", produto.nome, produto.quantidade); // Log do estoque
            return produto.quantidade > 0;
        }
    }

    fclose(fp);
    printf("Produto %s não encontrado no arquivo.\n", nome); // Log de falha
    return 0;
}

int produto_quantidade_disponivel(const char *nome, int quantidade) {
    Produto produto;
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            fclose(fp);
            return produto.quantidade >= quantidade;
        }
    }

    fclose(fp);
    return 0;
}

void abater_estoque(const char *nome, int quantidade) {
    Produto produto;
    FILE *fp_original = fopen("data/produtos.txt", "r");
    FILE *fp_temp = fopen("data/temp.txt", "w");

    if (fp_original == NULL || fp_temp == NULL) {
        printf("Erro ao abrir os arquivos para atualização do estoque.\n");
        return;
    }

    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            produto.quantidade -= quantidade; // Abate a quantidade vendida
        }
        fprintf(fp_temp, "%s %.2f %d |\n", produto.nome, produto.valor, produto.quantidade);
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/produtos.txt");
    rename("data/temp.txt", "data/produtos.txt");
}