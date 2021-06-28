INSERT INTO TBCOMPROMISSOS
            (
                 [ASSUNTO],
                 [LOCAL],
                [DATACOMPROMISSO],
                [HORAINICIO],
                [HORAFIM],
                [ID_CONTATO]


            )
            VALUES
            (
                @ASSUNTO,
                 @LOCAL,
                @DATACOMPROMISSO,
                @HORAINICIO,
                @HORAFIM,
                @ID_CONTATO
             )


SELECT 
            
            [ID],
                 [ASSUNTO],
                 [LOCAL],
                [DATACOMPROMISSO],
                [HORAINICIO],
                [HORAFIM],
                Contato.nome as "Nome Contato"

             FROM TBCompromissos JOIN TBContatos Contato  
             ON TBCompromissos.Id_Contato = Contato.id


            WHERE DATACOMPROMISSO < Date.Now;

          
UPDATE TBCOMPROMISSOS
	        SET	
            
		    [ASSUNTO] = @ASSUNTO,
                 [LOCAL] = @LOCAL,
                [DATACOMPROMISSO] = @DATACOMPROMISSO,
                [HORAINICIO] = @HORAINICIO,
                [HORAFIM] = @HORAFIM,
                [ID_CONTATO] = @ID_CONTATO
            

	        WHERE 
		    [ID] = @ID