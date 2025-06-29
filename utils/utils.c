#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stddef.h>
#include <time.h>
#include <stdbool.h>

#include "../include/participantes.h"
#include "../utils/utils.h"

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


/**
 * @brief Limpa o buffer de entrada
 * 
 * Esta função lê caracteres do stdin até encontrar um '\n' ou EOF,
 * garantindo que o buffer de entrada esteja limpo para a próxima leitura.
 */
void flush_in() {
    int ch;
    do {
        ch = fgetc(stdin);
    } while (ch != EOF && ch != '\n');
}


/**
 * @brief Verifica se uma string representa um número válido
 */
int is_valid_number(char str[], size_t length) {
    int valido = 1;
    for(int i = 0; i < length; i++) {
        if (str[i] < 48 || str[i] > 57) {
            valido = 0;
            break;
        }
    }
    return valido;
}


/**
 * @brief Formata o nome de um participante para que a primeira letra seja maiúscula e as demais minúsculas
 */
void pretty_format_name(char str[], size_t length) {
    if (length == 0) return;
    
    str[0] = toupper(str[0]);
    for (int i = 1; i < length; i++) {
        str[i] = tolower(str[i]);
    }
}

/**
 * @brief Valida o CPF de um participante
 * 
 * Verifica se o CPF tem 11 dígitos e se é um número válido.
 * Também verifica se o CPF já está cadastrado no sistema.
 * 
 * @param cpf O CPF a ser validado
 * @param length O tamanho do CPF (deve ser 11)
 * @return char Retorna 0 se o CPF é válido e já está cadastrado, 1 se não está cadastrado, -1 se inválido
 */
char validate_cpf(char cpf[], size_t length) {
    Participante participante;
    if (length != 11) {
        return -1;
    }

    if (!is_valid_number(cpf, length)) {
        return -1;
    }

    FILE *fp;

    fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        printf("Arquivo não encontrado.\n");
        return 1;
    }

    while (fscanf(fp, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {   
        if(strcmp(cpf, participante.cpf) == 0) {
            fclose(fp);
            return 0;
        }
    }

    fclose(fp);
    return 1;
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
int validar_tamanho_string(char *str, int min, int max) {
    fflush(stdin);

    int len = strlen(str);

    if (len < min || len > max) {
        return 1; // String inválida
    }

    return 0; // String válida
}



#if defined(_WIN32) || defined(_WIN64)

int validar_com_regex(const char *str, const char *pattern) {
    // Não disponivel no windows, apenas retorne válido
    return 0;
}

#else

#include <regex.h>

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
#endif

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

/**
 * @brief Compara uma data no formato dd/mm/yyyy com a data atual
 * 
 * @param data A data no formato dd/mm/yyyy
 * @return int Retorna 1 se a data já passou, 0 caso contrário
 */
int comparar_data(const char *data) {
    int dia_evento, mes_evento, ano_evento;
    int dia_atual, mes_atual, ano_atual;

    // Converte a string da data do evento para dia, mês e ano
    sscanf(data, "%d/%d/%d", &dia_evento, &mes_evento, &ano_evento);

    // Obtém a data atual
    time_t t = time(NULL);
    struct tm *data_atual = localtime(&t);
    dia_atual = data_atual->tm_mday;
    mes_atual = data_atual->tm_mon + 1; // Meses começam em 0
    ano_atual = data_atual->tm_year + 1900;

    // Compara os anos
    if (ano_evento < ano_atual) {
        return 1; // Evento já passou
    } else if (ano_evento > ano_atual) {
        return 0; // Evento ainda não passou
    }

    // Compara os meses (se o ano for igual)
    if (mes_evento < mes_atual) {
        return 1; // Evento já passou
    } else if (mes_evento > mes_atual) {
        return 0; // Evento ainda não passou
    }

    // Compara os dias (se o ano e o mês forem iguais)
    if (dia_evento < dia_atual) {
        return 1; // Evento já passou
    }

    return 0; // Evento ainda não passou
}


/**
 * @brief Valida se uma data no formato dd/mm/yyyy é válida
 * 
 * @param data A string contendo a data no formato dd/mm/yyyy
 * @return bool Retorna true se a data for válida, false caso contrário
 */
bool validar_data(const char *data) {
    int dia, mes, ano;

    // Verifica se a data está no formato correto
    if (sscanf(data, "%2d/%2d/%4d", &dia, &mes, &ano) != 3) {
        return false;
    }

    // Verifica se o ano é válido
    if (ano < 1900 || ano > 2100) {
        return false;
    }

    // Verifica se o mês é válido
    if (mes < 1 || mes > 12) {
        return false;
    }

    // Verifica se o dia é válido para o mês
    int dias_no_mes[] = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

    if (dia < 1 || dia > dias_no_mes[mes - 1]) {
        return false;
    }

    return true;
}