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

#endif // UTILS_H