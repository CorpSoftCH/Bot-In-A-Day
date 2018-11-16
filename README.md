# Bot-In-A-Day
This Repository contains all the materials needed in the Bot in a Day training in cooperation with Microsoft

## Erste Schritte

1. Öffnen Solution Bot-In-A-Day\PublicTransportBot_Start\PublicTransportBot.sln
2. F5 drücken
3. Bot Emulator v4 starten
4. Bot-In-A-Day\PublicTransportBot_Start\PublicTransportBot\BotConfiguration.bot öffnen
5. Mit dem Bot chatten
6. Debug-Modus in Visual Studio beenden

## LUIS

1. Mit einer Live-Id bei eu.luis.ai einloggen
2. Eine neue LUIS App erstellen
3. 2 Intents erfassen: "Greeting" und "Insult"
4. Beliebige weitere Intents erfassen
5. Utterances für alle Intents erfassen
6. LUIS App trainieren
7. LUIS App publishen
8. In Visual Studio zwei neue Klassen erstellen: LUIS.cs und LUISResponse.cs
9. Kopieren Inhalt Bot-In-A-Day\CodeSnippets\ LUIS.cs und LUISResponse.cs in die jeweiligen Files
10. Kopieren LUIS API Connection String und einfügen in LUIS.cs auf Zeile 18:
```C#
string RequestURI = "###ENTER YOUR LUIS APP CONNECTION STRING HERE###" + Query;
```
11. Kopieren Inhalt Bot-In-A-Day\CodeSnippets\LUISCall.cs auf die Zeilen NACH folgendem Code (ca. Zeilen 38/39/40):
```C#
var userInput = activity.Text;
string senderName = activity.From.Name;
string userReplyString = "";
```
12. F5 drücken
13. Bot Emulator v4 starten
14. Mit dem Bot chatten
15. Debug Modus in Visual Studio beenden

## Public Transport API

1. Einen neuen Intent in LUIS erfassen: "GetTransportConnection"
2. Eine Hierarchical Entity namens "City" erfassen, mit zwei Childs: "From" und "To"
3. Utterances für GetTransportConnection erfassen
4. LUIS App trainieren
5. LUIS App publishen
6. In Visual Studio zwei neue Klassen erstellen: TransportAPI.cs und TransportResponse.cs
7. Kopieren Inhalt Bot-In-A-Day\CodeSnippets\ TransportAPI.cs und TransportResponse.cs in die jeweiligen Files
8. Kopieren Inhalt Bot-In-A-Day\CodeSnippets\GetTransportConnection.cs auf die Zeilen NACH folgendem Code (ca. Zeilen 44-50):
```C#
switch (luisResponse.topScoringIntent.intent)
                {
```
9. F5 drücken
10. Bot Emulator v4 starten
11. Mit dem Bot chatten
12. Debug Modus in Visual Studio beenden
