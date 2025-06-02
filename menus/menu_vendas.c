#include <stdio.h>
#include <stdlib.h>

void menu_vendas() {
    int opcao;
    do {
        printf("\033[2J\033[H"); // Limpa a tela
        printf("\n==============================\n");
        printf("      MENU DE VENDAS (CRUD)\n");
        printf("==============================\n");
        printf("[1] ➤ Criar Venda\n");
        printf("[2] ➤ Listar Vendas\n");
        printf("[3] ➤ Atualizar Venda\n");
        printf("[4] ➤ Deletar Venda\n");
        printf("[5] ⇦ Voltar ao menu principal\n");
        printf("[6] ✖ Encerrar o programa\n");
        printf("------------------------------\n");
        printf("Escolha uma opção: ");
        scanf("%d", &opcao);    
        getchar();
        printf("------------------------------\n");
        switch (opcao) {
            case 1:
                printf("Criar Venda selecionado.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
                break;
            case 2:
                printf("Listar Vendas selecionado.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
                break;
            case 3:
                printf("Atualizar Venda selecionado.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
                break;
            case 4:
                printf("Deletar Venda selecionado.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
                break;
            case 5:
                printf("Voltando ao menu principal...\n");
                break;
            case 6:
                printf("\nObrigado por usar o sistema!\n");
                printf("Pressione ENTER para finalizar...");
                getchar();
                exit(0);
            default:
                printf("\nOpção inválida. Tente novamente.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
        }
    } while (opcao != 5);
}
