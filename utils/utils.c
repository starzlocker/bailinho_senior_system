#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/**
 * @brief Pausa a execução do programa até que o usuário pressione Enter
 * 
 * Esta função limpa o buffer de entrada e aguarda que o usuário
 * pressione a tecla Enter para continuar a execução.
 */
void pausar() {
    printf("\nPressione Enter para continuar...");
    while (getchar() != '\n'); // Limpa o buffer
    getchar(); // Aguarda Enter
}


/**
 * @brief Limpa a tela do console
 * 
 * Tô usando Linux, então tive que adaptar
 */
void limpar_tela() {
    #ifdef _WIN32
        // Para Windows
        system("cls");
    #else
        // Para Linux/Unix
        system("clear");
    #endif
}