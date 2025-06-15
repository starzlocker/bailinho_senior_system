#ifndef UTILS_H
#define UTILS_H


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

#endif // UTILS_H