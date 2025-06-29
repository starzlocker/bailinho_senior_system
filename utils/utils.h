#ifndef UTILS_H
#define UTILS_H

#include <stddef.h>
/**
 * @brief Pausa a execução do programa até que o usuário pressione Enter
 * 
 * Esta função limpa o buffer de entrada e aguarda que o usuário
 * pressione a tecla Enter para continuar a execução.
 */
void pausar();


/**
 * @brief Limpa a tela do console
 * 
 * Tô usando Linux, então tive que adaptar
 */
void limpar_tela();

/**
 * @brief Limpa o buffer de entrada
 * 
 * Esta função lê caracteres do stdin até encontrar um '\n' ou EOF,
 * garantindo que o buffer de entrada esteja limpo para a próxima leitura.
 */
void flush_in();

/**
 * @brief Verifica se uma string representa um número válido
 */
int is_valid_number(char str[], size_t length);

/**
 * @brief Formata o nome de um participante para que a primeira letra seja maiúscula e as demais minúsculas
 */
void pretty_format_name(char str[], size_t length);

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
char validate_cpf(char cpf[], size_t length);

/**
 * @brief Valida a quantidade de caracteres
 * 
 * @param str String a ser validada
 * @param int Quantidade mínima de caracteres
 * @param int Quantidade máxima de caracteres
 * 
 * @return int 1 se a string for inválida, 0 se for válida
 */
int validar_tamanho_string(char *str, int min, int max);

/**
 * @brief Valida uma string com expressão regular
 * 
 * @param str String a ser validada
 * @param pattern Padrão regex a ser utilizado na validação
 * 
 * @return int 0 se a string bater com o padrão, 1 se não bater
 */
int validar_com_regex(const char *str, const char *pattern);

/**
 * @brief Verifica se uma string contém espaços
 * 
 * @param str String a ser verificada
 * 
 * @return int 1 se a string contém espaços, 0 se não contém
 */
int contem_espaco(const char *str);

/**
 * @brief Compara uma data no formato dd/mm/yyyy com a data atual
 * 
 * @param data A data no formato dd/mm/yyyy
 * @return int Retorna 1 se a data já passou, 0 caso contrário
 */
int comparar_data(const char *data);

/**
 * @brief Valida se uma data no formato dd/mm/yyyy é válida
 * 
 * @param data A string contendo a data no formato dd/mm/yyyy
 * @return bool Retorna true se a data for válida, false caso contrário
 */
bool validar_data(const char *data);


#endif // UTILS_H
