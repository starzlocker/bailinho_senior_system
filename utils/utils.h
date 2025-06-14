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
 * @brief Valida a quantidade de caracteres
 * 
 * @param str String a ser validada
 * @param int Quantidade mínima de caracteres
 * @param int Quantidade máxima de caracteres
 * 
 * @return int 1 se a string for inválida, 0 se for válida
 */
int validar_string(char *str, int min, int max);

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

#endif // UTILS_H
