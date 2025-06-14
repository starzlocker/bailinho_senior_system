#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <regex.h>

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


// Valdações

/**
 * @brief Valida a quantidade de caracteres
 * 
 * @param str String a ser validada
 * @param int Quantidade mínima de caracteres
 * @param int Quantidade máxima de caracteres
 * 
 * @return int 1 se a string for inválida, 0 se for válida
 */
int validar_string(char *str, int min, int max) {
    fflush(stdin);

    int len = strlen(str);

    if (len < min || len > max) {
        return 1; // String inválida
    }

    return 0; // String válida
}

/**
 * @brief Valida uma string com expressão regular
 * 
 * @param str String a ser validada
 * @param pattern Padrão regex a ser utilizado na validação
 * 
 * @return int 0 se a string bater com o padrão, 1 se não bater
 */
int validar_com_regex(const char *str, const char *pattern) {
    regex_t regex;
    int resultado;

    // Compila o padrão regex
    resultado = regcomp(&regex, pattern, REG_EXTENDED);
    if (resultado) {
        printf("Erro ao compilar regex.\n");
        return 0;
    }

    // Compara a string com o padrão
    resultado = regexec(&regex, str, 0, NULL, 0);
    regfree(&regex);

    // Se bater, retorna 0, senão 1
    return (resultado != 0);
}

/**
 * @brief Verifica se uma string contém espaços
 * 
 * @param str String a ser verificada
 * 
 * @return int 1 se a string contém espaços, 0 se não contém
 */
int contem_espaco(const char *str) {
    while (*str) {
        if (*str == ' ') {
            return 1;  // Encontrou espaço
        }
        str++;
    }
    return 0;  // Não encontrou espaço
}
