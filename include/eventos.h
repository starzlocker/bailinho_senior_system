#ifndef EVENTOS_H
#define EVENTOS_H

/**
 * @struct Participante
 * @brief Estrutura que armazena os dados de um participante
 * 
 */
typedef struct {
    char nome[50];
    char data[11]; // 09/06/2024
    char horario[6]; // 14:30
    float valorEntrada;
    char status[10]; // "concluido" - "cancelado" - "agendado"
} Evento;


/**
 * @brief Cadastra um novo evento no sistema
 * 
 * Solicita ao usuário informações sobre o novo evento e
 * as armazena em um arquivo de texto (eventos.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int eventos_create();

/**
 * @brief Lista todos os eventos cadastrados
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int eventos_list();

// /**
//  * @brief Atualiza os dados de um participante específico
//  * 
//  * Solicita o CPF do participante a ser atualizado e, se encontrado,
//  * permite a edição de todos os seus dados. Utiliza um arquivo temporário
//  * para fazer a atualização.
//  * 
//  * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
//  */
// int participantes_update();

// /**
//  * @brief Apaga um participante específico pelo CPF
//  * 
//  * Solicita o CPF do participante a ser removido e o exclui do arquivo.
//  * Utiliza um arquivo temporário para a operação de exclusão.
//  * 
//  * @return int 0 em caso de sucesso, 1 em caso de erro
//  */
// int participantes_delete();

#endif /* EVENTOS_H */
