#ifndef PRODUTOS_H
#define PRODUTOS_H

/**
 * @struct Produto
 * @brief Estrutura que armazena os dados de um produto
 * 
 */
typedef struct {
    char nome[50];
    float valor;
    int quantidade;
} Produto;


/**
 * @brief Cadastra um novo produto no sistema
 * 
 * Solicita ao usuário informações sobre o novo produto e
 * armazena as informacoes do produto em um arquivo de texto (produtos.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_create();

/** 
 * @brief Lista todos os produtos cadastrados 
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_list();

/**
 * @brief Atualiza os dados de um produto específico  
 * 
 * Solicita o nome do produto a ser atualizado e, se encontrado,
 * permite a edição de todos os seus dados. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int produtos_update();

/**
 * @brief Apaga um produto específico pelo nome
 * 
 * Solicita o nome do produto a ser removido e o exclui do arquivo.
 * Utiliza um arquivo temporário para a operação de exclusão.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int produtos_delete();

#endif /* PRODUTOS_H */
