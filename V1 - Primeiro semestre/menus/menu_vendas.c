#include <stdio.h>
#include <stdlib.h>
#include "../include/vendas.h"

void menu_vendas() {
    int opcao;
    do {
        printf("\033[2J\033[H"); // Limpa a tela
        printf("\n==============================\n");
        printf("      MENU DE VENDAS (CRUD)\n");
        printf("==============================\n");
        printf("[1] ➤ Criar Venda\n");
        printf("[2] ➤ Listar Vendas\n");
        printf("[3] ➤ Deletar Venda\n");
        printf("[4] ⇦ Voltar ao menu principal\n");
        printf("[5] ✖ Encerrar o programa\n");
        printf("------------------------------\n");
        printf("Escolha uma opção: ");
        scanf("%d", &opcao);    
        getchar();
        printf("------------------------------\n");
        switch (opcao) {
            case 1:
                vendas_create();
                break;
            case 2:
                vendas_list();
                break;
            case 3:
                vendas_delete();
                break;
            case 4:
                printf("Voltando ao menu principal...\n");
                break;
            case 5:
                printf("\nObrigado por usar o sistema!\n");
                printf("Pressione ENTER para finalizar...");
                getchar();
                exit(0);
            default:
                printf("\nOpção inválida. Tente novamente.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
        }
    } while (opcao != 4);
}
