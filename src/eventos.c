#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <locale.h>
#include <stdbool.h>

#include "../include/eventos.h"
#include "../utils/utils.h"

int validaEvento; // Variável global para validação de entradas

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
    bool digitou_corretamente = false;

    fp = fopen("../../data/eventos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    do {
        printf("Digite o nome do evento (Sem espaços): ");
        scanf(" %49[^\n]", evento.nome);

        validaEvento = validar_string(evento.nome, 1, 49);

        if (validaEvento) {
            printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
            continue;
        }

        validaEvento = contem_espaco(evento.nome);

        if (validaEvento) {
            printf("Nome inválido! Não deve conter espaços.\n");
        }
    } while (validaEvento == 1);

    do {
        printf("Digite a data do evento (ex: 30/05/2003): ");
        scanf("%s", evento.data);

        validaEvento = validar_com_regex(evento.data, "^([0-2][0-9]|(3)[0-1])/(0[1-9]|1[0-2])/[0-9]{4}$");
        if (validaEvento) {
            printf("Data inválida! Deve estar no formato dd/mm/YY.\n");
        }
    } while (validaEvento == 1);

    do {
        printf("Digite o horário do evento (ex: 15:45): ");
        scanf("%s", evento.horario);

        validaEvento = validar_com_regex(evento.horario, "^([01][0-9]|2[0-3]):[0-5][0-9]$");
        if (validaEvento) {
            printf("Horário inválido! Deve estar no formato HH:MM 24horas.\n");
        }
    } while (validaEvento == 1);

    do {
        validaEvento = 0;
        printf("Digite o valor do evento: ");
        scanf("%f", &evento.valorEntrada);

        if (evento.valorEntrada < 0) {
            printf("Valor inválido! Valor do evento deve ser um número positivo.\n");
            validaEvento = 1;
        }
    } while (validaEvento == 1);

    listar_status_evento();

    do {
        validaEvento = 0;
        int status;

        printf("Digite o status do evento: ");
        scanf("%d", &status);
        strcpy(evento.status, status_evento(status));

        validaEvento = strcmp(evento.status, "invalido") == 0;

        if (validaEvento) {
            printf("Status inválido! Escolha um status válido.\n");
        }
    } while (validaEvento);

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

    fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de eventos:\n");
    printf("|-----------------------------------------------------------------------------------------------|\n");
    printf("| %-40s | %-12s | %-7s | %-13s | %-9s |\n", "Nome", "Data", "horario", "Entrada", "Status");
    printf("|-----------------------------------------------------------------------------------------------|\n");

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        printf("| %-40s | %-12s | %-7s | R$ %-10.2f | %-9s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
    }
    printf("|-----------------------------------------------------------------------------------------------|\n");

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
    int encontrado = 0;

    printf("Digite o nome do evento que deseja alterar o status: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("../../data/eventos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("../../data/temp.txt", "w");
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

            do {
                int status;

                printf("Digite o status do evento (atual: %s): ", evento.status);
                scanf("%d", &status);
                strcpy(evento.status, status_evento(status));

                validaEvento = strcmp(evento.status, "invalido") == 0;

                if (validaEvento) {
                    printf("Status inválido! Escolha um status válido.\n");
                }
            } while (validaEvento);

            fprintf(fp_temp, "%s %s %s %f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("../../data/eventos.txt");
    rename("../../data/temp.txt", "../../data/eventos.txt");

    if (encontrado == 0) {
        printf("Evento não encontrado.\n");
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
    printf("║ [1] Concluido                ║\n");
    printf("║ [2] Cancelado                ║\n");
    printf("║ [3] Agendado                 ║\n");
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
        default:
            return "invalido";
    }
}

int evento_existe(const char *nome) {
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(evento.nome, nome) == 0) {
            fclose(fp);
            return 1;
        }
    }

    fclose(fp);
    return 0;
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

int evento_passado(const char *nome) {
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(evento.nome, nome) == 0) {
            fclose(fp);

            // Usa a função comparar_data para verificar se o evento já passou
            return comparar_data(evento.data);
        }
    }

    fclose(fp);
    return 0;
}
