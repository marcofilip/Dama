## Dama C#
Il progetto è un semplice programma scritto in C# per simulare il classico gioco da tavolo Dama.  

# Regole
Il gioco è a turni, quindi servono 2 giocatori.  
Le regole sono semplici: chi riesce a mangiare tutti i pezzi dell’avversario vince.  
Le regole del gioco originali sono presenti:  
- Se si può mangiare un pezzo avversario, si è obbligati a farlo;  
- Se un pezzo arriva dalla parte dell’avversario, esso diventa un damone, che può muoversi sia all’indietro che su ogni casella obliqua.
# L'interfaccia semplice mostra:  
- I pezzi mangiati accumulati;  
- Il numero di vittorie per ciascun giocatore.
# Tecniche C# utilizzate
- Ereditarietà: La classe “Casella” è ereditata da Button, una classe contenente all’interno di Windows Forms, per ottenere maggiore controllo sui pulsanti.
- Molteplici Form: Più form sono state utilizzate. Una per l’intro iniziale (splash screen), una per il gioco e l’ultima per visualizzare il vincitore.
- Eventi: Per far funzionare le caselle, dobbiamo assegnare a ciascuna un “evento” che manda in esecuzione una funzione quando cliccata.
Fine
