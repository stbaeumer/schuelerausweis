# SchuelerFoto-NRW #

Mit ***SchuelerFoto-NRW*** können Sie klassenweise Fotos von Schülern in die SchILD-NRW-Datenbank übertragen.

![schild.png](https://bitbucket.org/repo/8zXbGdX/images/3262660000-schild.png)

## Ausführen ohne Installation

Eine Installation ist nicht erforderlich. Starten Sie durch Doppelklick auf die Datei ***SchuelerFoto-NRW.exe***. 

***Wichtig:*** Die Datei ***SchuelerFoto-NRW.exe*** muss im Verzeichnis ***SchILD-NRW*** liegen und kann nur dort erfolgreich ausgeführt werden. Entsprechende Windows-Berechtigungen im Verzeichnis ***SchILD-NRW*** sind Voraussetzung für die Nutzung von ***SchuelerFoto-NRW***.

![ordner.png](https://bitbucket.org/repo/8zXbGdX/images/1997397150-ordner.png)

## Das Programm nach dem Start

Zunächst müssen Sie die Lizenzbedingungen akzeptieren. Das gewählte Lizenzmodell garantiert Ihnen eine kostenlose Nutzung sowie Zugang zum Quelltext. Die Haftung wird ausgeschlossen.

![lizenz.png](https://bitbucket.org/repo/8zXbGdX/images/823073715-lizenz.png)

Wahlweise können Sie die geforderte Anzahl JPG-/JPEG-Bilder anfassen und auf die Programmfläche ziehen oder Sie ziehen einen einzelnen Ordner auf die Programmfläche. Der Ordner muss dann die korrekte Anzahl JPG-/JPEG-Bilder enthalten. Sollte der Ordner noch weitere Dateien enthalten, werden diese ignoriert. 

![start.png](https://bitbucket.org/repo/8zXbGdX/images/1068548820-start.png)

Sobald Sie Fotos oder einen Ordner mit Fotos mit gedrückter Maustaste auf die Fläche von ***SchuelerFoto-NRW*** ziehen, gibt Ihnen das Programm eine Rückmeldung. Entweder ist alles ok ...

![start-ok.png](https://bitbucket.org/repo/8zXbGdX/images/1182029275-start-ok.png)

... oder ein Problem wird erkannt und in rot beschrieben. Die Abbildungen unten zeigen zum einen ein Problem beim Ziehen von Bilddateien ...

![start-fehler.PNG](https://bitbucket.org/repo/8zXbGdX/images/274232229-start-fehler.PNG)

... und zum anderen ein Problem beim Ziehen eines ganzen Ordners. Wenn ein Ordner noch andere Dateien enthält (z. B. `picasa.ini`), dann ist das kein Problem. Nur die JPG-/JPEG-Dateien werden gezählt und berücksichtigt.

![start-fehler2.png](https://bitbucket.org/repo/8zXbGdX/images/3404731616-start-fehler2.png)

Wenn Sie die Maustaste über einer roten Meldung loslassen, passiert nichts. Sie können dann den beschriebenen Fehler korrigieren und erneut Dateien mit der Maus auf die Programmfläche ziehen. Wenn Sie die Maustaste über einer grünen Meldung loslassen beginnt die Verarbeitung. Von den Originalbildern werden dann quadratisch zugeschnittene Kopien im Format 160*160 Pixel erstellt und nach SchILD-NRW geladen. Je nach Rechnerleistung müssen Sie nun durchaus 10 Sekunden warten. Der Stand der Verarbeitung wird angezeigt. Für die Dauer der Verarbeitung wird die Auswahlbox für Klassen deaktiviert.

![start-go.png](https://bitbucket.org/repo/8zXbGdX/images/4103765185-start-go.png)

Anschließend wird der Erfolg quittiert. So können eine weitere Klasse wählen.

![verarbeitungAbgeschlossen.PNG](https://bitbucket.org/repo/8zXbGdX/images/2687206740-verarbeitungAbgeschlossen.PNG)

## Sicherheit

Vermutlich werden Sie von Ihrem Windows davor gewarnt ***SchuelerFoto-NRW.exe*** auszuführen. Je nach Version von Windows sieht die Warnung unterschiedlich aus. Ignorieren Sie die Warnung und führen Sie ***SchuelerFoto-NRW*** im ***SchILD-NRW***-Ordner aus. Falls Sie unsicher sind, ob Sie dem Programm vertrauen können, können Sie den Quelltext [hier](https://bitbucket.org/stbaeumer/schuelerfoto-nrw/src) einsehen und bei Bedarf selbst kompilieren.

![warnung.png](https://bitbucket.org/repo/8zXbGdX/images/53768194-warnung.png)

## Kosten

***SchuelerFoto-NRW*** ist dauerhaft kostenlos. Das Programm steht Ihnen unter den Bedingungen der [Gnu Public License Version 3](https://gnu.org/) zur Verfügung. Lesen Sie die Lizenzbedingungen, um mehr zu erfahren.

## Vorgehen

### Schritt 1: 

Fotografieren Sie ***alle*** Schüler einer Klasse ***in der richtigen Reihenfolge*** mit einer beliebigen digitalen (Smartphone-) Kamera. Sollte ein Schüler nicht anwesend sein oder die Zustimmung verweigern, fotografieren Sie evtl. die weiße Leinwand, damit die ***Reihenfolge und Anzahl*** der Fotos mit der ***Reihenfolge und Anzahl*** der Schüler übereinstimmt.

### Schritt 2: 

Wählen Sie in ***SchuelerFoto-NRW*** diejenige Klasse, deren Bilder Sie nach SchILD übertragen wollen. In der folgenden Abbildung  ist die Wahl auf die Klasse AOP1A mit insgesamt 27 Schülern gefallen:

![klasse.png](https://bitbucket.org/repo/8zXbGdX/images/489967876-klasse.png)

### Schritt 3: 

Öffnen Sie den Bilderordner Ihrer Kamera in Windows und ziehen Sie die Bilder nach ***SchuelerFoto-NRW***:

![ordner.png](https://bitbucket.org/repo/8zXbGdX/images/3506458232-ordner.png)

Alternativ können Sie auch einen einzelnen Ordner nach ***SchuelerFoto-NRW*** ziehen. Dieser Ordner muss dann für jeden Schüler der Klasse ein Foto enthalten. Er darf auch noch weitere Dateien enthalten. Wichtig ist nur, dass die Anzahl der JPG-/JPEG-Bilddateien exakt der ***Reihenfolge und Anzahl*** der Schüler entspricht.

![ordner2.PNG](https://bitbucket.org/repo/8zXbGdX/images/3039130096-ordner2.PNG)

### Schritt 4: 

Fertig! Gehen Sie nach SchILD und überprüfen Sie das Ergebnis, indem Sie den Aktualisieren-Schalter drücken. Wiederholen Sie den Vorgang für weitere Klassen. 

![aktualisieren.png](https://bitbucket.org/repo/8zXbGdX/images/1093220087-aktualisieren.png)

# Häufig gestellte Fragen #

## Was ist, wenn ich die Bilder der ganzen Klasse nachträglich ändern möchte?

> Das ist kein Problem. Wiederholen Sie das Vorgehen für die Klasse. Die alten Fotos werden dann durch die neuen Fotos ersetzt. Die Änderungen sind sofort in SchILD überprüfbar.  

## ***SchuelerFoto-NRW*** funktioniert seit dem letzten SchILD-Update nicht mehr. Woran liegt das? 

> ***SchuelerFoto-NRW*** ist so programmiert, dass es bei einem Schild-Update automatisch seinen Dienst einstellt. So wird sichergestellt, dass niemand mit einer veralteten Version von ***SchuelerFoto-NRW*** in eine (möglicherweise veränderte) SchILD-Datenbank schreibt. Sie müssen also nach jedem SchILD-Update die aktuelle ***SchuelerFoto-NRW***-Version herunterladen und erneut im SchILD-NRW-Ordner ablegen und ausführen. Kontaktieren Sie den Autor, wenn Sie glauben, dass ***SchuelerFoto-NRW*** aktualisiert werden muss. Über ***Hilfe>Info*** finden Sie die Version der Datenbank in SchILD:

![version.png](https://bitbucket.org/repo/8zXbGdX/images/4213710161-version.png)

## Wir nutzen eine andere Datenbank als ***SchILD2000n.MDB***, kann ich ***SchuelerFoto-NRW nutzen?***

> Nein. ***SchuelerFoto-NRW*** ist so programmiert, dass es nur mit ***SchiLD2000n.MDB*** zusammenarbeitet. Fühlen Sie sich frei den Quelltext anzupassen, damit auch Ihre Datenbank unterstützt wird. 

## Ich kann die Fotos nicht nach ***SchuelerFoto-NRW*** ziehen, weil mir das Foto eines Schülers fehlt! Was nun? ####

> Die Zuordnung kann nur vorgenommen werden, wenn die ***Anzahl und Reihenfolge*** der Schüler und die ***Anzahl und Reihenfolge*** der Fotos übereinstimmen. Evtl. fotografieren Sie zukünftig die leere Leinwand, wenn ein Schüler laut Liste fehlt oder seine Zustimmung verweigert. Eine schnelle Lösung des konkreten Problems kann darin liegen eine Kopie des Fotos des vorherigen Schülers am selben Ort wieder einzufügen. Die Kopie sortiert sich automatisch an der Stelle des fehlenden Schülers ein. Somit sind ***Anzahl und Reihenfolge*** korrekt. Vergessen Sie nicht die Kopie aus SchILD wieder zu entfernen. 

![kopie.png](https://bitbucket.org/repo/8zXbGdX/images/1503292237-kopie.png)

## Wenn ich den Quelltext selbst kompiliere, werde ich nach einem Kennwort gefragt. Was nun?

![password.png](https://bitbucket.org/repo/8zXbGdX/images/3384616216-password.png)

> Die Access-Datenbank ist herstellerseitig kennwortgeschützt. Das Kennwort wird Ihnen nicht im Quelltext mitgeliefert und kann auch nicht per E-Mail bei mir erfragt werden. Nur wenn Sie das Kennwort kennen und eintippen, können Sie eine Verbindung vom selbst-kompilierten ***SchuelerFoto-NRW*** zur Datenbank herstellen.

## ***SchuelerFoto-NRW*** meldet eine erfolgreiche Verarbeitung, aber die Fotos werden in SchILD-NRW nicht angezeigt. Was nun?

> Sie müssen den ***Aktualisieren***-Schalter drücken und dann zu einem Schüler-Datensatz wechseln.

![aktualisieren.png](https://bitbucket.org/repo/8zXbGdX/images/1210399464-aktualisieren.png)

## Wird die Datenbank durch das Hochladen der Bilder belastet?

> Ein typischen Handyfoto mit einer Größe von etwa 4 MB wird von ***SchuelerFoto-NRW*** auf ca. 50 kB reduziert. Dadurch wird sich die Datenbank nur unwesentlich vergrößern. 

## Wie sieht es mit dem Datenschutz aus?

> ***SchuelerFoto-NRW*** ist so programmiert, dass es lediglich personenbezogene Daten vom Benutzer entgegennimmt. Die Anzeige der Daten passiert dann ausschließlich durch SchILD-NRW. Weiterhin ist es nur Mitarbeitern mit Berechtigung zum Zugriff auf den ***SchILD-NRW***-Ordner möglich ***SchuelerFoto-NRW*** aufzurufen. Insofern nimmt ***SchuelerFoto-NRW*** keinerlei Einfluss auf Ihr bisheriges Datenschutz-Konzept. 

## Ich habe einen Fehler entdeckt. Wie kann ich den melden?

> Im ***SchILD-NRW***-Ordner finden Sie eine Datei namens ***SchuelerFoto.log***. Dort werden Fehlermeldungen gespeichert. Schicken Sie die Fehlermeldung an:

> ![mail.PNG](https://bitbucket.org/repo/8zXbGdX/images/2004706068-mail.PNG)