#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <ctype.h>
#include <locale.h>

#include "menus/menu_eventos.c"
#include "menus/menu_participantes.c"
#include "menus/menu_produtos.c"
#include "menus/menu_vendas.c"

#define MAX_INPUT 256

int main()
{
    //muda a localização para Português do Brasil
    setlocale(LC_ALL, "portuguese");

    char input[MAX_INPUT];
    int opcao;

    do {
        printf("\033[2J\033[H"); // Limpa a tela
        printf("\n========================================\n");
        printf("      SISTEMA DE GERENCIAMENTO\n");
        printf("========================================\n");
        printf("[1] ➤ Eventos\n");
        printf("[2] ➤ Participantes\n");
        printf("[3] ➤ Produtos\n");
        printf("[4] ➤ Vendas\n");
        printf("[5] ✖ Sair\n");
        printf("----------------------------------------\n");
        printf("Escolha uma opção: ");
        scanf("%d", &opcao);
        getchar();
        printf("----------------------------------------\n");
        switch (opcao) {
            case 1:
                menu_eventos();
                break;
            case 2:
                menu_participantes();
                break;
            case 3:
                menu_produtos();
                break;
            case 4:
                menu_vendas();
                break;
            case 5:
                printf("\nObrigado por usar o sistema!\n");
                printf("Pressione ENTER para finalizar...");
                getchar();
                break;
            default:
                printf("\nOpção inválida. Tente novamente.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
        }
    } while(opcao != 5);

    return 0;
}