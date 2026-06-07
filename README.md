# Wypożyczalnia aut

## 1. Opis celu projektu
Program rozwiązuje problem ręcznego śledzenia dostępności samochodów, oraz błędu ludzkiego podczas naliczania kosztów (różne stawki, różne rodzaje pojazdów, kary za brak paliwa/rozładowaną baterię).
Aplikacja jest skierowana do firm zajmujących się wypożyczaniem aut.

### Główne funkcjonalności:
<ul>
  <li>Obsługa wypożyczeń i zwrotów.</li>
  <li>Trwały zapis i odczyt stanu z pliku .json.</li>
  <li>Dodawanie i przeglądanie pojazdów i klientów.</li>
  <li>Sprawdzenie listy aktualnie dostępnych aut z filtorwaniem.</li>
  <li>Automatyczne wyliczanie kosztów oraz kar na podstawie wybranej reguły.</li>
</ul>

## 2. Architektura i struktura obiektowa

### Diagram UML
<img width="1951" height="910" alt="obraz" src="https://github.com/user-attachments/assets/c04dba17-fcca-4b50-863d-c652e639c2bc" />


### Opis najważniejszych klas i komponentów:
* **`Wypozyczalnia`:** Klasa centralna (Fasada logiki biznesowej) zarządzająca kolekcjami pojazdów, klientów oraz historią. Udostępnia interfejs operacyjny dla warstwy prezentacji.
* **`Pojazd` i pochodne `AutoSpalinowe`, `AutoElektryczne`:** Definiują parametry aut i sposób wyliczania kar dodatkowych.
* **`Wypozyczenie`:** Reprezentuje transakcję. Łączy klienta, auto, wybraną taryfę.
* **`PlikoweRepozytoriumJson`:** odpowiedzialna za odczyt danych startowych i serializację historii do formatu JSON.

Dziedziczenie: AutoSpalinowe i AutoElektryczne dziedziczą po abstrakcyjnej klasie Pojazd.

Agregacja: Klasa Wypozyczenie używa obiekty Klient, Pojazd oraz IRegulaOplat.

Kompozycja: Klasa Klient jest ściśle powiązany z klasą Wypozyczenie.

### Uzasadnienie decyzji projektowych:

* Każda klasa oraz interfejs są umieszczone w oddzielnym pliku. Ułatwia to przegladanie kodu i zapobiega sytuacjom, gdzie mamy np. 1000 linijek kodu w jednym pliku.
* Zastosowanie WinForms pozwoliło na stworzenie wygodnego GUI. Dzięki oknom, przyciskom i listom program jest intuicyjny i przejrzysty w przeciwieństwie do konsoli.
* Izolacja operacji plikowych w PlikoweRepozytoriumJson pozwala na ewentualną podmianę plików na bazę SQL w przyszłości. Użycie klasy WypozyczenieTemp rozwiązuje problem błędu cyklicznych referencji podczas zapisu do JSON.

## 3. Instrukcja uruchomienia i użycia

### Instrukcja uruchomienia
1. Sklonuj pliki źródłowe do jednego katalogu.
2. Upewnij się, że w katalogu aplikacji znajdują się pliki: `klienci.txt` oraz `auta.txt`.
3. Skompiluj i uruchom aplikację.

### Instrukcja użycia
* Aplikacja wymaga środowiska .NET. W folderze z plikiem wykonywalnym .exe muszą znajdować się pliki auta.txt oraz klienci.txt.
* Nowy klient: Wpisz imię i nazwisko i kliknij "Dodaj klienta".
* Wypożyczenie: Wybierz klienta, auto oraz rodzaj opłaty, a następnie zatwierdź wynajem.
* Zwrot: Wybierz aktywne wypożyczenie, wprowadź datę, stan paliwa/baterii i kliknij przycisk "Zwróć".
* Zapis: Kliknij przycisk zapisu, aby zapisać historię operacji do pliku JSON.


## 4. Podział pracy w zespole
Naszą pracę opieraliśmy na delegowaniu zadań i weryfikacji kodu. Lead Developer odpowiadał za kluczową logikę oraz rozdzielanie i ostateczne sprawdzanie kodu. Reszta zajmowała się implementacją przydzielonych funkcjonalności, w tym tworzeniem klas modeli, budową interfejsu graficznego oraz testowaniem gotowej aplikacji.
