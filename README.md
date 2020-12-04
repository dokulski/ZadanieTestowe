# Zadanie testowe


Aplikację wykonana z użyciem:
ms server 2016
vs 2019 (.net framework 4.7.2)
bibliotek:
Entity Framework 6.4.4,
WebApi Owin 5.2.7

W paczce znajdują się backup bazy danych(Pumox.bak) aplikacja serwera(Pumox.Server), aplikacja klienta(Pumox.Client)
aplikacja domyślnie nasłuchuje na porcie 8080

obsługuje następujące Uri na lokalhoście:

POST company/create
GET  companies 		 -> wszystkie wpisy
GET  companies/{id}  -> pojedyńczy wpisy
PUT  companies/{id}
DELETE companies/{id}
