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
    printf("Digite o status do evento (concluido, cancelado, agendado): ");
    scanf("%s", evento.status);

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


// /**
//  * @brief Atualiza os dados de um participante específico
//  * 
//  * Solicita o CPF do participante a ser atualizado e, se encontrado,
//  * permite a edição de todos os seus dados. Utiliza um arquivo temporário
//  * para fazer a atualização.
//  * 
//  * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
//  */
// int participantes_update() {
//     FILE *fp_original, *fp_temp;
//     Participante participante, new_participante;
//     char cpf_alvo[12];
//     int encontrado = 0;

//     printf("Digite o CPF do participante que deseja atualizar: ");
//     scanf("%s", cpf_alvo);

//     fp_original = fopen("data/participantes.txt", "r");
//     if (fp_original == NULL) {
//         printf("Erro ao abrir o arquivo.\n");
//         return 1;
//     }

//     fp_temp = fopen("data/temp.txt", "w");
//     if (fp_temp == NULL) {
//         printf("Erro ao criar arquivo temporário.\n");
//         fclose(fp_original);
//         return 1;
//     }

//     // Lê cada participante do arquivo original
//     while (fscanf(fp_original, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
//         if (strcmp(cpf_alvo, participante.cpf) != 0) {
//             fprintf(fp_temp, "%s %s %s %d |\n", participante.nome, participante.cpf, participante.celular, participante.idade);

//         } else {
//             encontrado = 1;
//             printf("Digite o nome do participante (atual: %s): ", participante.nome);
//             scanf("%s", new_participante.nome);
//             printf("Digite o CPF do participante (atual: %s): ", participante.cpf);
//             scanf("%s", new_participante.cpf);
//             printf("Digite o celular do participante (atual: %s): ", participante.celular);
//             scanf("%s", new_participante.celular);
//             printf("Digite a idade do participante (atual: %d): ", participante.idade);
//             scanf("%d", &new_participante.idade);
//             fprintf(fp_temp, "%s %s %s %d |\n", new_participante.nome, new_participante.cpf, new_participante.celular, new_participante.idade);
            
//         }
//     }
//     fclose(fp_original);
//     fclose(fp_temp);

//     remove("data/participantes.txt");
//     rename("data/temp.txt", "data/participantes.txt");

//     if (encontrado == 0) {
//         printf("Participante nâo encontrado.\n");
//         pausar();
//         limpar_tela();
//         return 1;
//     }

//     printf("Dados atualizados com sucesso!");
//     limpar_tela();
//     return 0;
// }


// /**
//  * @brief Apaga um participante específico pelo CPF
//  * 
//  * Solicita o CPF do participante a ser removido e o exclui do arquivo.
//  * Utiliza um arquivo temporário para a operação de exclusão.
//  * 
//  * @return int 0 em caso de sucesso, 1 em caso de erro
//  */
// int participantes_delete() {
//     FILE *fp_original, *fp_temp;
//     Participante participante;
//     char cpf_alvo[12];
//     int encontrado = 0;
//     char nome_alvo[50];

//     printf("Digite o CPF do participante que deseja apagar: ");
//     scanf("%s", cpf_alvo);

//     fp_original = fopen("data/participantes.txt", "r");
//     if (fp_original == NULL) {
//         printf("Erro ao abrir o arquivo.\n");
//         return 1;
//     }

//     fp_temp = fopen("data/temp.txt", "w");
//     if (fp_temp == NULL) {
//         printf("Erro ao criar arquivo temporário.\n");
//         fclose(fp_original);
//         return 1;
//     }

//     // Lê cada participante do arquivo original
//     while (fscanf(fp_original, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
//         // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
//         if (strcmp(participante.cpf, cpf_alvo) != 0) {
//             fprintf(fp_temp, "%s %s %s %d |\n", participante.nome, participante.cpf, participante.celular, participante.idade);
//         } else {
//             encontrado = 1;
//             strcpy(nome_alvo, participante.nome);
//         }
//     }

//     fclose(fp_original);
//     fclose(fp_temp);

//     // Exclui o arquivo original
//     remove("data/participantes.txt");
//     // Renomeia o arquivo temporário para o nome do arquivo original
//     rename("data/temp.txt", "data/participantes.txt");

//     if (encontrado) {
//         printf("Participante %s com CPF %s removido com sucesso!\n", nome_alvo, cpf_alvo);

//     } else {
//         printf("\n⚠️ participante com CPF %s não encontrado! ⚠️\n", cpf_alvo);
//     }

//     pausar();
//     limpar_tela();
    
//     return 0;
// }
