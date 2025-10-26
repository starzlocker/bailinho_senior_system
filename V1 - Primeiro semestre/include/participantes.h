#ifndef PARTICIPANTES_H
#define PARTICIPANTES_H

/**
 * @struct Participante
 * @brief Estrutura que armazena os dados de um participante
 * 
 */
typedef struct {
    char nome[50];
    char sobrenome[50];
    char cpf[12];
    char celular[15];
    int idade;
} Participante;


/**
 * @brief Cadastra um novo participante no sistema
 * 
 * Solicita ao usuário informações sobre o novo participante e
 * as armazena em um arquivo de texto (participantes.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_create();

/**
 * @brief Lista todos os participantes cadastrados
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_list();

/**
 * @brief Atualiza os dados de um participante específico
 * 
 * Solicita o CPF do participante a ser atualizado e, se encontrado,
 * permite a edição de todos os seus dados. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int participantes_update();

/**
 * @brief Apaga um participante específico pelo CPF
 * 
 * Solicita o CPF do participante a ser removido e o exclui do arquivo.
 * Utiliza um arquivo temporário para a operação de exclusão.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_delete();

/**
 * @brief Verifica se um participante com o CPF fornecido existe
 * 
 * @param cpf O CPF do participante
 * @return int 1 se o participante existir, 0 caso contrário
 */
int participante_existe(const char *cpf);

#endif /* PARTICIPANTES_H */
