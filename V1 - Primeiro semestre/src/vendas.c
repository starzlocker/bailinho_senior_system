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

// Implementação das funções de vendas

int vendas_create() {
    Venda venda;
    FILE *fp;

    fp = fopen("data/vendas.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de vendas.\n");
        return 1;
    }

    do {
        printf("Digite o CPF do participante: ");
        scanf("%s", venda.cpf_participante);

        if (!participante_existe(venda.cpf_participante)) {
            printf("⚠️ Participante com CPF %s não encontrado! Tente novamente.\n\n", venda.cpf_participante);
        }
    } while (!participante_existe(venda.cpf_participante));

    do {
        printf("Digite o nome do evento: ");
        scanf("%s", venda.nome_evento);

        if (!evento_existe(venda.nome_evento)) {
            printf("⚠️ Evento %s não encontrado! Tente novamente.\n\n", venda.nome_evento);
        } else if (evento_passado(venda.nome_evento)) {
            printf("⚠️ Evento %s já aconteceu! Não é possível realizar a venda.\n\n", venda.nome_evento);
        }
    } while (!evento_existe(venda.nome_evento) || evento_passado(venda.nome_evento));

    
    bool produto_encontrado = false;
    bool estoque_disponivel = false;

    do {
        printf("Digite o nome do produto: ");
        scanf("%s", venda.nome_produto);

        produto_encontrado = produto_existe(venda.nome_produto);
        estoque_disponivel = produto_estoque_disponivel(venda.nome_produto);
        
        if (!produto_encontrado) {
            printf("⚠️ Produto %s não encontrado! Tente novamente.\n\n", venda.nome_produto);
        } else if (!estoque_disponivel) {
            printf("⚠️ Produto %s está sem estoque! Não é possível realizar a venda.\n\n", venda.nome_produto);
        }
    } while (!produto_encontrado || !estoque_disponivel);

    do {
        printf("Digite a quantidade do produto: ");
        scanf("%d", &venda.quantidade);

        if (venda.quantidade <= 0) {
            printf("Quantidade inválida! Deve ser um número inteiro positivo.\n\n");
        } else if (!produto_quantidade_disponivel(venda.nome_produto, venda.quantidade)) {
            printf("⚠️ Estoque insuficiente para o produto %s! Tente novamente.\n\n", venda.nome_produto);
        }
    } while (venda.quantidade <= 0 || !produto_quantidade_disponivel(venda.nome_produto, venda.quantidade));

    // Abate a quantidade do produto no estoque
    abater_estoque(venda.nome_produto, venda.quantidade);

    fprintf(fp, "%s %s %s %d |\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);

    fclose(fp);
    limpar_tela();

    return 0;
}

int vendas_list() {
    Venda venda;
    FILE *fp;

    fp = fopen("data/vendas.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de vendas:\n");
    printf("╔════════════════════════════════════════════════════════════════════════════╗\n");
    printf("║ %-15s ║ %-20s ║ %-19s ║ %-10s ║\n", "CPF Participante", "Evento", "Produto", "Quantidade");
    printf("║----------------------------------------------------------------------------║\n");

    while (fscanf(fp, "%11s %19s %19s %d |", venda.cpf_participante, venda.nome_evento, venda.nome_produto, &venda.quantidade) == 4) {
        printf("║ %-16s ║ %-20s ║ %-19s ║ %-10d ║\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);
    }
    printf("╚════════════════════════════════════════════════════════════════════════════╝\n");

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}

int vendas_delete() {
    FILE *fp_original, *fp_temp;
    Venda venda;
    char cpf_alvo[12];
    char evento_alvo[50];
    int encontrado = 0;

    printf("Digite o CPF do participante da venda que deseja apagar: ");
    scanf("%s", cpf_alvo);

    printf("Digite o nome do evento da venda que deseja apagar: ");
    scanf("%s", evento_alvo);

    fp_original = fopen("data/vendas.txt", "r");
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

    while (fscanf(fp_original, "%11s %19s %19s %d |", venda.cpf_participante, venda.nome_evento, venda.nome_produto, &venda.quantidade) == 4) {
        if (strcmp(venda.cpf_participante, cpf_alvo) != 0 || strcmp(venda.nome_evento, evento_alvo) != 0) {
            fprintf(fp_temp, "%s %s %s %d |\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);
        } else {
            encontrado = 1;
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/vendas.txt");
    rename("data/temp.txt", "data/vendas.txt");

    if (encontrado) {
        printf("Venda removida com sucesso!\n");
    } else {
        printf("\n⚠️ Venda não encontrada! ⚠️\n");
    }

    pausar();
    limpar_tela();

    return 0;
}
