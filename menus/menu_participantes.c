#include <stdio.h>
#include <stdlib.h>
#include "../include/participantes.h"

void menu_participantes() {
  int opcao;
  do {
    printf("\033[2J\033[H"); // Limpa a tela

    printf("\n==============================\n");
    printf("      MENU DE PARTICIPANTES (CRUD)\n");
    printf("==============================\n");
    printf("[1] ➤ Criar Participante\n");
    printf("[2] ➤ Listar Participantes\n");
    printf("[3] ➤ Atualizar Participante\n");
    printf("[4] ➤ Deletar Participante\n");
    printf("[5] ⇦ Voltar ao menu principal\n");
    printf("[6] ✖ Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma opção: ");
    scanf("%d", &opcao);
    getchar();
    printf("------------------------------\n");

    switch (opcao) {
      case 1:
        participantes_create();
        break;

      case 2:
        participantes_list();
        break;

      case 3:
        participantes_update();
        break;

      case 4:
        participantes_delete();
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
