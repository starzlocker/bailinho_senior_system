#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../include/participantes.h"
#include <stddef.h>

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
