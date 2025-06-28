#include <stdio.h>
#include <stdlib.h>
#include "../include/produtos.h"
#include "../include/menu_produtos.h"

void menu_produtos() {
  int opcao;
  do {
    system("cls");

    printf("\n==============================\n");
    printf("     MENU DE PRODUTOS (CRUD)\n");
    printf("==============================\n");
    printf("[1] Criar Produto\n");
    printf("[2] Listar Produtos\n");
    printf("[3] Atualizar Produto\n");
    printf("[4] Deletar Produto\n");
    printf("[5] Voltar ao menu principal\n");
    printf("[6] Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma funcionalidade: ");
    scanf("%d", &opcao);
    getchar();
    printf("------------------------------\n");

    switch (opcao) {
      case 1:
        produtos_create();
        break;

      case 2:
        produtos_list();
        break;

      case 3:
        produtos_update();
        break;

      case 4:
        produtos_delete();
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
