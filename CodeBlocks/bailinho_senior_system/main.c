#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <ctype.h>
#include <locale.h>
#include <time.h>
#include <stddef.h>
#include <stdbool.h>

#define MAX_INPUT 256

int valida;

int validar_string(char *str, int min, int max);
int contem_espaco(const char *str);
int validar_com_regex(const char *str, const char *pattern);
void limpar_tela();
void pausar();
void flush_in();
void pretty_format_name(char str[], size_t length);
char validate_cpf(char cpf[], size_t length);
int is_valid_number(char str[], size_t length);

void menu_eventos();
int eventos_create();
int eventos_list();
int eventos_update();
void listar_status_evento();
const char* status_evento(int status);
int evento_existe(const char *nome);
int evento_passado(const char *nome);
typedef struct {
    char nome[50];
    char data[11]; // 09/06/2024
    char horario[6]; // 14:30
    float valorEntrada;
    char status[10]; // "concluido" - "cancelado" - "agendado"
} Evento;

void menu_participantes();
int participantes_create();
int participantes_list();
int participantes_update();
int participantes_delete();
int participante_existe(const char *cpf);
typedef struct {
    char nome[50];
    char sobrenome[50];
    char cpf[12];
    char celular[15];
    int idade;
} Participante;

void menu_produtos();
int produtos_create();
int produtos_list();
int produtos_update();
int produtos_delete();
int produto_existe(const char *nome);
int produto_estoque_disponivel(const char *nome);
int produto_quantidade_disponivel(const char *nome, int quantidade);
void abater_estoque(const char *nome, int quantidade);
typedef struct {
    char nome[50];
    float valor;
    int quantidade;
} Produto;

void menu_vendas();
int vendas_create();
int vendas_list();
int vendas_delete();
typedef struct {
    char cpf_participante[12];
    char nome_evento[50];
    char nome_produto[50];
    int quantidade;
} Venda;


int main()
{
    //muda a localização para Português do Brasil
    setlocale(LC_ALL, "portuguese");

    char input[MAX_INPUT];
    int opcao;

    do {
        system("cls"); // Limpa a tela
        printf("\n========================================\n");
        printf("      SISTEMA DE GERENCIAMENTO\n");
        printf("========================================\n");
        printf("[1] - Eventos\n");
        printf("[2] - Participantes\n");
        printf("[3] - Produtos\n");
        printf("[4] - Vendas\n");
        printf("[5] - Sair\n");
        printf("----------------------------------------\n");
        printf("Escolha uma opção: ");
        scanf("%d", &opcao);
        getchar();
        printf("----------------------------------------\n");
        switch (opcao) {
            case 1:
                menu_eventos();
                break;
            case 2:
                menu_participantes();
                break;
            case 3:
                menu_produtos();
                break;
            case 4:
                menu_vendas();
                break;
            case 5:
                printf("\nObrigado por usar o sistema!\n");
                printf("Pressione ENTER para finalizar...");
                getchar();
                break;
            default:
                printf("\nOpção inválida. Tente novamente.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
        }
    } while(opcao != 5);

    return 0;
}

int validar_string(char *str, int min, int max) {
    fflush(stdin);

    int len = strlen(str);

    if (len < min || len > max) {
        return 1; // String inválida
    }

    return 0; // String válida
}
int contem_espaco(const char *str) {
    while (*str) {
        if (*str == ' ') {
            return 1;  // Encontrou espaço
        }
        str++;
    }
    return 0;  // Não encontrou espaço
}
int validar_com_regex(const char *str, const char *pattern) {
    // Não disponivel no windows, apenas retorne válido
    return 0;
}
void limpar_tela() {
    #ifdef _WIN32
        // Para Windows
        system("cls");
    #else
        // Para Linux/Unix
        system("clear");
    #endif
}
void pausar() {
    printf("\nPressione Enter para continuar...");
    while (getchar() != '\n'); // Limpa o buffer
    getchar(); // Aguarda Enter
}
void flush_in() {
    int ch;
    do {
        ch = fgetc(stdin);
    } while (ch != EOF && ch != '\n');
}
void pretty_format_name(char str[], size_t length) {
    if (length == 0) return;

    str[0] = toupper(str[0]);
    for (int i = 1; i < length; i++) {
        str[i] = tolower(str[i]);
    }
}
char validate_cpf(char cpf[], size_t length) {
    Participante participante;
    if (length != 11) {
        return -1;
    }

    if (!is_valid_number(cpf, length)) {
        return -1;
    }

    FILE *fp;

    fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        printf("Arquivo não encontrado.\n");
        return 1;
    }

    while (fscanf(fp, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        if(strcmp(cpf, participante.cpf) == 0) {
            fclose(fp);
            return 0;
        }
    }

    fclose(fp);
    return 1;
}
int is_valid_number(char str[], size_t length) {
    int valido = 1;
    for(int i = 0; i < length; i++) {
        if (str[i] < 48 || str[i] > 57) {
            valido = 0;
            break;
        }
    }
    return valido;
}

void menu_eventos() {
  int opcao;

  do {
    system("cls"); // Limpa a tela

    printf("\n==============================\n");
    printf("      MENU DE EVENTOS (CRUD)\n");
    printf("==============================\n");
    printf("[1] - Criar Evento\n");
    printf("[2] - Listar Eventos\n");
    printf("[3] - Atualizar Evento\n");
    printf("[5] - Voltar ao menu principal\n");
    printf("[6] - Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma opção: ");
    scanf("%d", &opcao);
    getchar();
    printf("------------------------------\n");

    switch (opcao) {
      case 1:
        eventos_create();
        break;

      case 2:
        eventos_list();
        break;

      case 3:
        eventos_update();
        break;

      case 5:
        printf("Voltando ao menu principal...\n");
        break;

      case 6:
        printf("Encerrando o programa.\n");
        printf("Pressione ENTER para finalizar...");
        getchar();
        exit(0);

      default:
        printf("\nOpção inválida. Tente novamente.\n");
        printf("Pressione ENTER para continuar...");
        getchar();
    }
  } while (opcao != 5);
}
int eventos_create() {
    Evento evento;
    FILE *fp;

    fp = fopen("data/eventos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        pausar();
        return 1;
    }

    do {
        printf("Digite o nome do evento (Sem espaços): ");
        scanf(" %49[^\n]", evento.nome);

        valida = validar_string(evento.nome, 1, 49);

        if (valida) {
            printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
            continue;
        }

        valida = contem_espaco(evento.nome);

        if (valida) {
            printf("Nome inválido! Não deve conter espaços.\n");
        }
    } while (valida == 1);

    do {
        printf("Digite a data do evento (No formato dd/mm/YY, ex: 30/05/2003): ");
        scanf("%s", evento.data);

        valida = validar_com_regex(evento.data, "^([0-2][0-9]|(3)[0-1])/(0[1-9]|1[0-2])/[0-9]{4}$");
        if (valida) {
            printf("Data inválida! Deve estar no formato dd/mm/YY.\n");
        }
    } while (valida == 1);

    do {
        printf("Digite o horário do evento (No formato HH:MM 24horas, ex: 15:45): ");
        scanf("%s", evento.horario);

        valida = validar_com_regex(evento.horario, "^([01][0-9]|2[0-3]):[0-5][0-9]$");
        if (valida) {
            printf("Horário inválido! Deve estar no formato HH:MM 24horas.\n");
        }
    } while (valida == 1);

    do {
        valida = 0;
        printf("Digite o valor do evento (ex: 10,00): ");
        scanf("%f", &evento.valorEntrada);

        if (evento.valorEntrada < 0) {
            printf("Valor inválido! Valor do evento deve ser um número positivo.\n");
            valida = 1;
        }
    } while (valida == 1);

    listar_status_evento();

    do {
        int status;

        printf("Digite o status do evento: ");
        scanf("%d", &status);
        strcpy(evento.status, status_evento(status));

        valida = strcmp(evento.status, "invalido") == 0;

        if (valida) {
            printf("Status inválido! Escolha um status válido.\n");
        }
    } while (valida);

    fprintf(
        fp,
        "%s %s %s %f %s |\n",
        evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status
    );

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}
int eventos_list() {
    Evento evento;
    FILE *fp;
    char linha[256];

    fp = fopen("data/eventos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de eventos:\n");
    printf("||===================================================================================================||\n");
    printf("|| %-40s || %-12s || %-7s || %-13s || %-9s ||\n", "Nome", "Data", "horario", "Entrada", "Status");
    printf("||---------------------------------------------------------------------------------------------------||\n");

    while (fgets(linha, sizeof(linha), fp) != NULL) {
        if (sscanf(linha, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
            printf("|| %-40s || %-12s || %-7s || R$ %-10.2f || %-9s ||\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        }
    }
    printf("||===================================================================================================||\n");

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}
int eventos_update() {
    FILE *fp_original, *fp_temp;
    Evento evento;
    char nome_alvo[50];
    int encontrado = 0;

    printf("Digite o nome do evento que deseja alterar o status: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/eventos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        pausar();
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        pausar();
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

                valida = strcmp(evento.status, "invalido") == 0;

                if (valida) {
                    printf("Status inválido! Escolha um status válido.\n");
                }
            } while (valida);

            fprintf(fp_temp, "%s %s %s %f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/eventos.txt");
    rename("data/temp.txt", "data/eventos.txt");

    if (encontrado == 0) {
        printf("Evento nâo encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
    limpar_tela();
    return 0;
}
void listar_status_evento() {
    printf("\nStatus dos eventos:\n");
    printf("||===============================||\n");
    printf("|| [1] - Concluido              || \n");
    printf("|| [2] - Cancelado              || \n");
    printf("|| [3] - Agendado               ||\n");
    printf("||===============================||\n");
}
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
    FILE *fp = fopen("data/eventos.txt", "r");
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
int evento_passado(const char *nome) {
    Evento evento;
    FILE *fp = fopen("data/eventos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(evento.nome, nome) == 0) {
            fclose(fp);

            // Converte a data do evento para um formato comparável
            struct tm data_evento = {0};
            int dia, mes, ano;
            if (sscanf(evento.data, "%d/%d/%d", &dia, &mes, &ano) == 3) {
                data_evento.tm_mday = dia;
                data_evento.tm_mon = mes - 1;
                data_evento.tm_year = ano - 1900;
                data_evento.tm_hour = 0;
                data_evento.tm_min = 0;
                data_evento.tm_sec = 0;
                data_evento.tm_isdst = -1;
            } else {
                fclose(fp);
                return 0; // Se falhar parsing, considera que o evento não passou
            }

            // Converte para time_t
            time_t tempo_evento = mktime(&data_evento);


            // Obtém a data atual
            time_t tempo_atual = time(NULL);

            return difftime(tempo_evento, tempo_atual) < 0; // Retorna 1 se o evento já passou
        }
    }

    fclose(fp);
    return 0;
}

void menu_participantes() {
  int opcao;
  do {
    system("cls"); // Limpa a tela

    printf("\n==============================\n");
    printf("      MENU DE PARTICIPANTES (CRUD)\n");
    printf("==============================\n");
    printf("[1] - Cadastrar Participante\n");
    printf("[2] - Listar Participantes\n");
    printf("[3] - Atualizar Participante\n");
    printf("[4] - Deletar Participante\n");
    printf("[5] - Voltar ao menu principal\n");
    printf("[6] - Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma opção: ");
    scanf("%d", &opcao);
    getchar();
    printf("------------------------------\n");

    switch (opcao) {
      case 1:
        printf("Cadastrar Participante selecionado.\n\n");
        participantes_create();
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 2:
        printf("Listar Participantes selecionado.\n");
        participantes_list();
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 3:
        printf("Atualizar Participante selecionado.\n");
        participantes_update();
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 4:
        printf("Deletar Participante selecionado.\n");
        participantes_delete();
        printf("Pressione ENTER para continuar...");
        getchar();
        break;

      case 5:
        printf("Voltando ao menu principal...\n");
        break;

      case 6:
        printf("\nObrigado por usar o sistema!\n");
        printf("Pressione ENTER para finalizar...");
        getchar();
        exit(0);

      default:
        printf("\nOpção inválida. Tente novamente.\n");
        printf("Pressione ENTER para continuar...");
        getchar();
    }
  } while (opcao != 5);
}
int participantes_create() {
    Participante participante;
    FILE *fp;
    char cpf[12];
    char nome[50];
    char sobrenome[50];
    char is_cpf_valido = 0;


    fp = fopen("data/participantes.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("Digite o nome do participante: ");
    scanf("%49s", nome);
    flush_in();

    while (strlen(nome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o nome do participante: ");
        scanf("%49s", nome);
        flush_in();
    }

    pretty_format_name(nome, strlen(nome));
    strcpy(participante.nome, nome);

    printf("Digite o sobrenome do participante: ");
    scanf("%49s", sobrenome);
    flush_in();

    while (strlen(sobrenome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o sobrenome do participante: ");
        scanf("%49s", sobrenome);
        flush_in();
    }

    pretty_format_name(sobrenome, strlen(sobrenome));
    strcpy(participante.sobrenome, sobrenome);

    printf("Digite o CPF do participante: ");
    scanf("%11s", cpf);
    flush_in();

    is_cpf_valido = validate_cpf(cpf, strlen(cpf));

    while (is_cpf_valido != 1) {
        if (is_cpf_valido == 0) {
            printf("\nO CPF já está cadastrado!\nDigite o CPF do participante: ");
        } else if (is_cpf_valido == -1) {
            printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        }
        scanf("%11s", cpf);
        is_cpf_valido = validate_cpf(cpf, strlen(cpf));
        flush_in();
    }

    strcpy(participante.cpf, cpf);

    printf("Digite o celular do participante: ");
    scanf("%14s", participante.celular);
    flush_in();

    while (strlen(participante.celular) < 10 || !is_valid_number(participante.celular, strlen(participante.celular))) {
        printf("\nO celular deve ser composto por 10 ou mais números! \nDigite o celular do participante: ");
        scanf("%14s", participante.celular);
        flush_in();
    }

    printf("Digite a idade do participante: ");
    scanf("%d", &participante.idade);
    flush_in();

    while (participante.idade < 0 || participante.idade > 120) {
        printf("\nA idade deve ser um número entre 0 e 120! \nDigite a idade do participante: ");
        scanf("%d", &participante.idade);
        flush_in();
    }

    fprintf(
        fp,
        "%s %s %s %s %d |\n",
        participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);

    fclose(fp);
    limpar_tela();

    return 0;
}
int participantes_list() {
    Participante participante;
    FILE *fp;
    char linha[256];

    fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de participantes:\n");
    printf("--------------------------------------------------------------------------------------\n");
    printf("| %-20s | %-20s | %-11s | %-14s | %-5s |\n", "Nome", "Sobrenome", "CPF", "Celular", "Idade");
    printf("--------------------------------------------------------------------------------------\n");

    while (fgets(linha, sizeof(linha), fp) != NULL) {
        if (sscanf(linha, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
            printf("| %-20s | %-20s | %-11s | %-14s | %-5d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        }
    }

    printf("--------------------------------------------------------------------------------------\n");

    fclose(fp);
    return 0;
}
int participantes_update() {
    FILE *fp_original, *fp_temp;
    Participante participante, new_participante;
    char cpf_alvo[12];
    char nome[50];
    char sobrenome[50];
    int encontrado = 0;

    printf("Digite o CPF do participante que deseja atualizar: ");
    scanf("%11s", cpf_alvo);
    flush_in();
    while(strlen(cpf_alvo) != 11 || !is_valid_number(cpf_alvo, strlen(cpf_alvo))) {
        printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        scanf("%11s", cpf_alvo);
        flush_in();
    }


    fp_original = fopen("data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        pausar();
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        pausar();
        fclose(fp_original);
        return 1;
    }

    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        if (strcmp(cpf_alvo, participante.cpf) != 0) {
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;

            printf("Digite o nome do participante (atual: %s): ", participante.nome);
            scanf("%49s", nome);
            flush_in();

            while (strlen(nome) < 3) {
                printf("\nO nome deve ter 3 ou mais letras! \nDigite o nome do participante: ");
                scanf("%49s", nome);
                flush_in();
            }
            pretty_format_name(nome, strlen(nome));
            strcpy(new_participante.nome, nome);

            printf("Digite o sobrenome do participante (atual: %s): ", participante.sobrenome);
            scanf("%49s", sobrenome);
            flush_in();

            while (strlen(sobrenome) < 3) {
                printf("\nO nome deve ter 3 ou mais letras! \nDigite o sobrenome do participante: ");
                scanf("%49s", sobrenome);
                flush_in();
            }
            pretty_format_name(sobrenome, strlen(sobrenome));
            strcpy(new_participante.sobrenome, sobrenome);

            printf("Digite o CPF do participante (atual: %s): ", participante.cpf);
            scanf("%11s", new_participante.cpf);
            flush_in();

            if (strcmp(new_participante.cpf, participante.cpf) != 0) {
                char cpf_valido = validate_cpf(new_participante.cpf, strlen(new_participante.cpf));
                while (cpf_valido != 1) {
                    if (cpf_valido == 0) {
                        printf("CPF já cadastrado! Digite outro CPF: ");
                    } else {
                        printf("CPF inválido! Digite um CPF com 11 números: ");
                    }
                    scanf("%11s", new_participante.cpf);
                    flush_in();
                    cpf_valido = validate_cpf(new_participante.cpf, strlen(new_participante.cpf));
                }
            }

            printf("Digite o celular do participante (atual: %s): ", participante.celular);
            scanf("%14s", new_participante.celular);
            flush_in();

            while (strlen(new_participante.celular) < 10 || !is_valid_number(new_participante.celular, strlen(new_participante.celular))) {
                printf("\nO celular deve ser composto por 10 ou mais números! \nDigite o celular do participante: ");
                scanf("%14s", new_participante.celular);
                flush_in();
            }
            printf("Digite a idade do participante (atual: %d): ", participante.idade);
            scanf("%d", &new_participante.idade);
            flush_in();

            while (new_participante.idade < 0 || new_participante.idade > 120) {
                printf("\nA idade deve ser um número entre 0 e 120! \nDigite a idade do participante: ");
                scanf("%d", &new_participante.idade);
                flush_in();
            }

            fprintf(fp_temp, "%s %s %s %s %d |\n", new_participante.nome, new_participante.sobrenome, new_participante.cpf, new_participante.celular, new_participante.idade);
        }
    }
    fclose(fp_original);
    fclose(fp_temp);

    remove("data/participantes.txt");
    rename("data/temp.txt", "data/participantes.txt");

    if (encontrado == 0) {
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!\n");
    pausar();
    limpar_tela();
    return 0;
}
int participantes_delete() {
    FILE *fp_original, *fp_temp;
    Participante participante;
    char cpf_alvo[12];
    int encontrado = 0;
    char nome_alvo[50];

    printf("Digite o CPF do participante que deseja apagar: ");
    scanf("%11s", cpf_alvo);
    flush_in();
    while(strlen(cpf_alvo) != 11 || !is_valid_number(cpf_alvo, strlen(cpf_alvo))) {
        printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        scanf("%11s", cpf_alvo);
        flush_in();
    }

    fp_original = fopen("data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        pausar();
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        pausar();
        fclose(fp_original);
        return 1;
    }

    // Lê cada participante do arquivo original
    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
        if (strcmp(participante.cpf, cpf_alvo) != 0) {
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;
            strcpy(nome_alvo, participante.nome);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Exclui o arquivo original
    remove("data/participantes.txt");
    // Renomeia o arquivo temporário para o nome do arquivo original
    rename("data/temp.txt", "data/participantes.txt");

    if (encontrado) {
        printf("Participante %s com CPF %s removido com sucesso!\n", nome_alvo, cpf_alvo);

    } else {
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
    }

    pausar();
    limpar_tela();

    return 0;
}
int participante_existe(const char *cpf) {
    Participante participante;
    FILE *fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        if (strcmp(participante.cpf, cpf) == 0) {
            fclose(fp);
            return 1;
        }
    }

    fclose(fp);
    return 0;
}

void menu_produtos() {
  int opcao;
  do {
    limpar_tela(); // Limpa a tela

    printf("\n==============================\n");
    printf("     MENU DE PRODUTOS (CRUD)\n");
    printf("==============================\n");
    printf("[1] - Criar Produto\n");
    printf("[2] - Listar Produtos\n");
    printf("[3] - Atualizar Produto\n");
    printf("[4] - Deletar Produto\n");
    printf("[5] - Voltar ao menu principal\n");
    printf("[6] - Encerrar o programa\n");
    printf("------------------------------\n");
    printf("Escolha uma opção: ");
    scanf("%d", &opcao);
    getchar();
    printf("------------------------------\n");

    switch (opcao) {
      case 1:
        produtos_create();
        break;

      case 2:
        produtos_list();
        break;

      case 3:
        produtos_update();
        break;

      case 4:
        produtos_delete();
        break;

      case 5:
        printf("Voltando ao menu principal...\n");
        break;

      case 6:
        printf("\nObrigado por usar o sistema!\n");
        printf("Pressione ENTER para finalizar...");
        getchar();
        exit(0);

      default:
        printf("\nOpção inválida. Tente novamente.\n");
        printf("Pressione ENTER para continuar...");
        getchar();
    }
  } while (opcao != 5);
}
int produtos_create() {
    Produto produto;
    FILE *fp;

    fp = fopen("data/produtos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    do {
        printf("Digite o nome do produto (sem espaço): ");
        scanf(" %49[^\n]", produto.nome);

        valida = validar_string(produto.nome, 1, 49);

        if (valida) {
            printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
            continue;
        }

        valida = contem_espaco(produto.nome);

        if (valida) {
            printf("Nome inválido! Não deve conter espaços.\n");
        }
    } while (valida == 1);

    do {
        valida = 0;
        printf("Digite o valor do produto: ");
        scanf("%f", &produto.valor);

        if (produto.valor < 0) {
            printf("Valor inválido! Valor do produto deve ser um número positivo.\n");
            valida = 1;
        }
    } while (valida == 1);

    do {
        valida = 0;
        printf("Digite a quantidade do produto: ");
        scanf("%d", &produto.quantidade);

        if (produto.quantidade < 0) {
            printf("Quantidade inválida! Deve ser um número inteiro positivo.\n");
            valida = 1;
        }
    } while (valida == 1);

    fprintf(fp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}
int produtos_list() {
    Produto produto;
    FILE *fp;
    char linha[256];

    fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de produtos:\n");
    printf("||================================================================================||\n");
    printf("|| %-40s || %-15s || %-15s ||\n", "Nome", "Valor", "Quantidade");
    printf("||--------------------------------------------------------------------------------||\n");

    while (fgets(linha, sizeof(linha), fp) != NULL) {
        if (sscanf(linha, "%49s %f %d", produto.nome, &produto.valor, &produto.quantidade) == 3) {
            printf("|| %-40s || R$ %-12.2f || %-15d ||\n", produto.nome, produto.valor, produto.quantidade);
        }
    }
    printf("||================================================================================||\n");

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}
int produtos_update() {
    FILE *fp_original, *fp_temp;
    Produto produto, new_produto;
    char nome_alvo[50];
    int encontrado = 0;

    printf("Digite o nome do produto que deseja atualizar: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/produtos.txt", "r");
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

    // Lê cada produto do arquivo original
    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(nome_alvo, produto.nome) != 0) {
            fprintf(fp_temp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);

        } else {
            encontrado = 1;

            do {
                printf("Digite o novo nome do produto (sem espaços - atual: %s): ", produto.nome);
                scanf(" %49[^\n]", new_produto.nome);

                valida = validar_string(new_produto.nome, 1, 49);

                if (valida) {
                    printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n");
                    continue;
                }

                valida = contem_espaco(new_produto.nome);

                if (valida) {
                    printf("Nome inválido! Não deve conter espaços.\n");
                }
            } while (valida == 1);

            do {
                valida = 0;
                printf("Digite o novo valor do produto (atual: R$ %.2f): ", produto.valor);
                scanf("%f", &new_produto.valor);

                if (new_produto.valor < 0) {
                    printf("Valor inválido! Valor do produto deve ser um número positivo.\n");
                    valida = 1;
                }
            } while (valida == 1);

            do {
                valida = 0;
                printf("Digite a nova quantidade do produto (atual: %d): ", produto.quantidade);
                scanf("%d", &new_produto.quantidade);

                if (new_produto.quantidade < 0) {
                    printf("Quantidade inválida! Deve ser um número inteiro positivo.\n");
                    valida = 1;
                }
            } while (valida == 1);

            fprintf(fp_temp, "%s %f %d |\n", new_produto.nome, new_produto.valor, new_produto.quantidade);

        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/produtos.txt");
    rename("data/temp.txt", "data/produtos.txt");

    if (encontrado == 0) {
        printf("Produto não encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
    pausar();
    limpar_tela();
    return 0;
}
int produtos_delete() {
    FILE *fp_original, *fp_temp;
    Produto produto;
    char nome_alvo[50];
    int encontrado = 0;

    printf("Digite o nome do produto que deseja apagar: ");
    scanf("%s", nome_alvo);

    fp_original = fopen("data/produtos.txt", "r");
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

    // Lê cada produto do arquivo original
    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
        if (strcmp(produto.nome, nome_alvo) != 0) {
            fprintf(fp_temp, "%s %f %d |\n", produto.nome, produto.valor, produto.quantidade);
        } else {
            encontrado = 1;
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Exclui o arquivo original
    remove("data/produtos.txt");
    // Renomeia o arquivo temporário para o nome do arquivo original
    rename("data/temp.txt", "data/produtos.txt");

    if (encontrado) {
        printf("Produto %s removido com sucesso!\n", nome_alvo);
    } else {
        printf("\n Produto %s não encontrado! \n", nome_alvo);
    }

    pausar();
    limpar_tela();

    return 0;
}
int produto_existe(const char *nome) {
    Produto produto;
    char linha[256]; // Buffer para armazenar cada linha do arquivo
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro: Não foi possível abrir o arquivo de produtos.\n");
        return 0;
    }

    while (fgets(linha, sizeof(linha), fp)) {

        // Substitui vírgulas por pontos na linha lida
        for (char *p = linha; *p; p++) {
            if (*p == '.') {
                *p = ',';
            }
        }

        int result = sscanf(linha, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade);
        if (result == 3) {
            if (strcmp(produto.nome, nome) == 0) {
                fclose(fp);
                return 1;
            }
        }
    }

    fclose(fp);
    return 0;
}
int produto_estoque_disponivel(const char *nome) {
    Produto produto;
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de produtos.\n");
        pausar();
        return 0;
    }

    while (fscanf(fp, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            fclose(fp);
            printf("\nEstoque disponível para %s: %d\n", produto.nome, produto.quantidade); // Log do estoque
            return produto.quantidade > 0;
        }
    }

    fclose(fp);
    printf("Produto %s não encontrado no arquivo.\n", nome);
    pausar(); // Log de falha
    return 0;
}
int produto_quantidade_disponivel(const char *nome, int quantidade) {
    Produto produto;
    FILE *fp = fopen("data/produtos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            fclose(fp);
            return produto.quantidade >= quantidade;
        }
    }

    fclose(fp);
    return 0;
}
void abater_estoque(const char *nome, int quantidade) {
    Produto produto;
    FILE *fp_original = fopen("data/produtos.txt", "r");
    FILE *fp_temp = fopen("data/temp.txt", "w");

    if (fp_original == NULL || fp_temp == NULL) {
        printf("Erro ao abrir os arquivos para atualização do estoque.\n");
        pausar();
        return;
    }

    while (fscanf(fp_original, "%49s %f %d |", produto.nome, &produto.valor, &produto.quantidade) == 3) {
        if (strcmp(produto.nome, nome) == 0) {
            produto.quantidade -= quantidade; // Abate a quantidade vendida
        }
        fprintf(fp_temp, "%s %.2f %d |\n", produto.nome, produto.valor, produto.quantidade);
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/produtos.txt");
    rename("data/temp.txt", "data/produtos.txt");
}

void menu_vendas() {
    int opcao;
    do {
        limpar_tela(); // Limpa a tela
        printf("\n==============================\n");
        printf("      MENU DE VENDAS (CRUD)\n");
        printf("==============================\n");
        printf("[1] - Criar Venda\n");
        printf("[2] - Listar Vendas\n");
        printf("[3] - Deletar Venda\n");
        printf("[4] - Voltar ao menu principal\n");
        printf("[5] - Encerrar o programa\n");
        printf("------------------------------\n");
        printf("Escolha uma opção: ");
        scanf("%d", &opcao);
        getchar();
        printf("------------------------------\n");
        switch (opcao) {
            case 1:
                vendas_create();
                break;
            case 2:
                vendas_list();
                break;
            case 3:
                vendas_delete();
                break;
            case 4:
                printf("Voltando ao menu principal...\n");
                break;
            case 5:
                printf("\nObrigado por usar o sistema!\n");
                printf("Pressione ENTER para finalizar...");
                getchar();
                exit(0);
            default:
                printf("\nOpção inválida. Tente novamente.\n");
                printf("Pressione ENTER para continuar...");
                getchar();
        }
    } while (opcao != 4);
}
int vendas_create() {
    Venda venda;
    FILE *fp;

    fp = fopen("data/vendas.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de vendas.\n");
        pausar();
        return 1;
    }

    do {
        printf("Digite o CPF do participante: ");
        scanf("%s", venda.cpf_participante);

        if (!participante_existe(venda.cpf_participante)) {
            printf(" Participante com CPF %s não encontrado! Tente novamente.\n\n", venda.cpf_participante);
        }
    } while (!participante_existe(venda.cpf_participante));

    do {
        printf("Digite o nome do evento: ");
        scanf("%s", venda.nome_evento);

        if (!evento_existe(venda.nome_evento)) {
            printf(" Evento %s não encontrado! Tente novamente.\n\n", venda.nome_evento);
        } else if (evento_passado(venda.nome_evento)) {
            printf(" Evento %s já aconteceu! Não é possível realizar a venda.\n\n", venda.nome_evento);
        }
    } while (!evento_existe(venda.nome_evento) || evento_passado(venda.nome_evento));


    bool produto_encontrado = false;
    bool estoque_disponivel = false;

    do {
        printf("Digite o nome do produto: ");
        scanf("%s", venda.nome_produto);

        produto_encontrado = produto_existe(venda.nome_produto);
        estoque_disponivel = produto_estoque_disponivel(venda.nome_produto);

        if (!produto_encontrado) {
            printf(" Produto %s não encontrado! Tente novamente.\n\n", venda.nome_produto);
        } else if (!estoque_disponivel) {
            printf(" Produto %s está sem estoque! Não é possível realizar a venda.\n\n", venda.nome_produto);
        }
    } while (!produto_encontrado || !estoque_disponivel);

    do {
        printf("Digite a quantidade do produto: ");
        scanf("%d", &venda.quantidade);

        if (venda.quantidade <= 0) {
            printf("Quantidade inválida! Deve ser um número inteiro positivo.\n\n");
        } else if (!produto_quantidade_disponivel(venda.nome_produto, venda.quantidade)) {
            printf(" Estoque insuficiente para o produto %s! Tente novamente.\n\n", venda.nome_produto);
        }
    } while (venda.quantidade <= 0 || !produto_quantidade_disponivel(venda.nome_produto, venda.quantidade));

    // Abate a quantidade do produto no estoque
    abater_estoque(venda.nome_produto, venda.quantidade);

    fprintf(fp, "%s %s %s %d |\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);

    fclose(fp);
    limpar_tela();

    return 0;
}
int vendas_list() {
    Venda venda;
    FILE *fp;

    fp = fopen("data/vendas.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("\nLista de vendas:\n");
    printf("||===============================================================================||\n");
    printf("|| %-15s || %-20s || %-19s || %-10s ||\n", "CPF Participante", "Evento", "Produto", "Quantidade");
    printf("||-------------------------------------------------------------------------------||\n");

    while (fscanf(fp, "%11s %19s %19s %d |", venda.cpf_participante, venda.nome_evento, venda.nome_produto, &venda.quantidade) == 4) {
        printf("|| %-16s || %-20s || %-19s || %-10d ||\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);
    }
    printf("||===============================================================================||\n");

    fclose(fp);
    pausar();
    limpar_tela();

    return 0;
}
int vendas_delete() {
    FILE *fp_original, *fp_temp;
    Venda venda;
    char cpf_alvo[12];
    char evento_alvo[50];
    int encontrado = 0;

    printf("Digite o CPF do participante da venda que deseja apagar: ");
    scanf("%s", cpf_alvo);

    printf("Digite o nome do evento da venda que deseja apagar: ");
    scanf("%s", evento_alvo);

    fp_original = fopen("data/vendas.txt", "r");
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

    while (fscanf(fp_original, "%11s %19s %19s %d |", venda.cpf_participante, venda.nome_evento, venda.nome_produto, &venda.quantidade) == 4) {
        if (strcmp(venda.cpf_participante, cpf_alvo) != 0 || strcmp(venda.nome_evento, evento_alvo) != 0) {
            fprintf(fp_temp, "%s %s %s %d |\n", venda.cpf_participante, venda.nome_evento, venda.nome_produto, venda.quantidade);
        } else {
            encontrado = 1;
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    remove("data/vendas.txt");
    rename("data/temp.txt", "data/vendas.txt");

    if (encontrado) {
        printf("Venda removida com sucesso!\n");
    } else {
        printf("\n Venda não encontrada! \n");
    }

    pausar();
    limpar_tela();

    return 0;
}
