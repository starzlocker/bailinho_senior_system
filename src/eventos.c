#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../include/eventos.h"
#include "../utils/utils.h"


/**
 * @brief Cadastra um novo evento no sistema
 * 
 * Solicita ao usuário informações sobre o novo evento e
 * as armazena em um arquivo de texto (eventos.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int eventos_create() {
    Evento evento;
    FILE *fp;
    int status;

    fp = fopen("data/eventos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("Digite o nome do evento (Sem espaços): ");
    scanf("%s", evento.nome);
    printf("Digite a data do evento (No formato dd/mm/YY, ex: 30/05/2003): ");
    scanf("%s", evento.data);
    printf("Digite o horário do evento (No formato HH:MM 24horas, ex: 15:45): ");
    scanf("%s", evento.horario);
    printf("Digite o valor do evento: ");
    scanf("%f", &evento.valorEntrada);
    listar_status_evento();
    printf("Digite o status do evento: ");
    scanf("%d", &status);

    strcpy(evento.status, status_evento(status));

    fprintf(
        fp,
        "%s %s %s %f %s |\n",
        evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status
    );

    fclose(fp);
    limpar_tela();

    return 0;
}


/**
 * @brief Lista todos os eventos cadastrados
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int eventos_list() {
    Evento evento;
    FILE *fp;

    fp = fopen("data/eventos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de eventos:\n");
    printf("╔═══════════════════════════════════════════════════════════════════════════════════════════════╗\n");
    printf("║ %-40s ║ %-12s ║ %-7s ║ %-13s ║ %-9s ║\n", "Nome", "Data", "horario", "Entrada", "Status");
    printf("║-----------------------------------------------------------------------------------------------║\n");

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        printf("║ %-40s ║ %-12s ║ %-7s ║ R$ %-10.2f ║ %-9s ║\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
    }
    printf("╚═══════════════════════════════════════════════════════════════════════════════════════════════╝\n");

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}


/**
 * @brief Atualiza os status de um evento
 * 
 * Solicita o nome do evento a ser atualizado e, se encontrado,
 * permite a alteração do seu status. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int eventos_update() {
    FILE *fp_original, *fp_temp;
    Evento evento;
    char nome_alvo[50];
    int encontrado = 0, status;

    printf("Digite o nome do evento que deseja alterar o status: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/eventos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    //Lê cada evento do arquivo original
    while (fscanf(fp_original, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(nome_alvo, evento.nome) != 0) {
            fprintf(fp_temp, "%s %s %s %f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);

        } else {
            encontrado = 1;
            listar_status_evento();
            printf("Digite o status do evento (atual: %s): ", evento.status);
            scanf("%d", &status);

            strcpy(evento.status, status_evento(status));
            fprintf(fp_temp, "%s %s %s %f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        }
    }
    fclose(fp_original);
    fclose(fp_temp);

    remove("data/eventos.txt");
    rename("data/temp.txt", "data/eventos.txt");

    if (encontrado == 0) {
        printf("Participante nâo encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
    limpar_tela();
    return 0;
}

/**
 * @brief Lista os status dos eventos disponíveis
 * 
 * Exibe uma tabela com os status possíveis para os eventos,
 * incluindo "Concluido", "Cancelado" e "Agendado".
 * 
 * @return void
 */
void listar_status_evento() {
    printf("\nStatus dos eventos:\n");
    printf("╔══════════════════════════════╗\n");
    printf("║ [1] ➤ Concluido              ║\n");
    printf("║ [2] ➤ Cancelado              ║\n");
    printf("║ [3] ➤ Agendado               ║\n");
    printf("╚══════════════════════════════╝\n");
}

/**
 * @brief Converte o status numérico de um evento para uma string
 * 
 * Recebe um inteiro representando o status do evento e retorna uma string
 * 
 * @param status O status do evento (1: concluido, 2: cancelado, 3: agendado)
 * 
 * @return const char* A string correspondente ao status do evento
 */
const char* status_evento(int status) {
    switch (status) {
        case 1:
            return "concluido";
        case 2:
            return "cancelado";
        case 3:
            return "agendado";
    }
}
