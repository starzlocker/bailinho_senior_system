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

/**
 * @brief Atualiza os status de um evento
 * 
 * Solicita o nome do evento a ser atualizado e, se encontrado,
 * permite a alteração do seu status. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int eventos_update();

/**
 * @brief Lista os status dos eventos disponíveis
 * 
 * Exibe uma tabela com os status possíveis para os eventos,
 * incluindo "Concluido", "Cancelado" e "Agendado".
 * 
 * @return void
 */
void listar_status_evento();

/**
 * @brief Converte o status numérico de um evento para uma string
 * 
 * Recebe um inteiro representando o status do evento e retorna uma string
 * 
 * @param status O status do evento (1: concluido, 2: cancelado, 3: agendado)
 * 
 * @return const char* A string correspondente ao status do evento
 */
const char* status_evento(int status);

#endif /* EVENTOS_H */
