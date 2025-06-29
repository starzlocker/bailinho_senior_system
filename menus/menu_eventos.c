#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

#include "../include/eventos.h"
#include "../include/menu_eventos.h"

void menu_eventos() {

    int opcao;
    do {
        system("cls");

        printf("\n==============================\n");
        printf("      MENU DE EVENTOS \n");
        printf("==============================\n");
        printf("[1] Criar Evento\n");
        printf("[2] Listar Eventos\n");
        printf("[3] Atualizar Evento\n");
        printf("[4] Voltar ao menu principal\n");
        printf("[5] Encerrar o programa\n");
        printf("------------------------------\n");
        printf("Escolha uma funcionalidade: ");
        scanf("%d", &opcao);
        getchar();
        printf("------------------------------\n");

        switch (opcao) {
            case 1:
                eventos_create();
            break;

            case 2:
                eventos_list();
            break;

            case 3:
                eventos_update();
            break;

            case 4:
                printf("Voltando ao menu principal...\n");
            break;

            case 5:
                printf("Encerrando o programa.\n");
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
