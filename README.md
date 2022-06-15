# pomodoro

e uma API par se gerenciar o progresso de metas com base na técnica de pomodoros 

o consumir a API primeiro se cadastra no sistema e ganha um login 

com esse login ele pode acessar o end-point de login para se logar e receber receber um token

com esse token e possível  acessar os outros recursos da API 

- um usuário pode criar uma meta que e automaticamente associada a esse usuário pelo token
    
    ao criar uma meta e retornado a meta cadastrada 
    
- com a meta cadastrada e possível cadastrar uma tarefa
    
    para cadastrar uma tarefa e preciso informa o id de uma meta 
    
    se for informado o id de uma meta que não esta associada ao usuário da ação e retornado um erro de não autorizado  
    
    se não existir uma meta com aquele ID e retornado um erro de NotFound()
    
    existe tres tipos de tarefa a tarefa base a recursiva e a rotina 
    
    ![Untitled](pomodoro%20f9ef983c48874456859484f0e0efeeda/Untitled.png)
    
    ![Untitled](pomodoro%20f9ef983c48874456859484f0e0efeeda/Untitled%201.png)
    
    ![Untitled](pomodoro%20f9ef983c48874456859484f0e0efeeda/Untitled%202.png)
    
- com a tarefa cadastrada voce pode criar um pomodoro

![Untitled](pomodoro%20f9ef983c48874456859484f0e0efeeda/Untitled%203.png)