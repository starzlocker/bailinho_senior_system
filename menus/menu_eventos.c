#include <stdio.h>
#include <stdlib.h>
#include "../include/eventos.h"

void menu_eventos() {
  int opcao;

  do {
    printf("\033[2J\033[H"); // Limpa a tela

    printf("\n==============================\n");
    printf("      MENU DE EVENTOS (CRUD)\n");
    printf("==============================\n");
    printf("[1] ➤ Criar Evento\n");
    printf("[2] ➤ Listar Eventos\n");
    printf("[3] ➤ Atualizar Evento\n");
    printf("[4] ➤ Deletar Evento\n");
    printf("[5] ⇦ Voltar ao menu principal\n");
    printf("[6] ✖ Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma opção: ");
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
        printf("Atualizar Evento selecionado.\n");
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 4:
        printf("Deletar Evento selecionado.\n");
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 5:
        printf("Voltando ao menu principal...\n");
        break;

      case 6:
        printf("Encerrando o programa.\n");
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
