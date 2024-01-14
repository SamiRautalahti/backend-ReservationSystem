# ReservationSystem2022

"AL00CM24-3001 Back-end työkalut 29.8.2022-31.12.2022"

Ohessa kurssin harjoitustyö.

Harjoitustyö on siis varaussovelluksen backend, jolla käyttäjät voivat varata muiden käyttäjien luomia kohteita (esim tila tai tavara).

    Käyttäjän luominen (huomioi, että tälle backend-funktiolle täytyy lähettää salasana, vaikka sitä ei käyttäjätietojen mukana muuten siirretäkään)
    Käyttäjän tietojen hakeminen/muokkaaminen/poistaminen
        Omia tietoja voi muokata/poistaa
    Varattavan kohteen luominen/muokkaaminen/poistaminen
        Kohteella on omistaja, joka voi muokata/poistaa. Muut käyttäjät vain näkevät kohteen
    Kohteen varaaminen
        Tässä tarkistettava, ettei samalla kohteella ole samalle ajalle jo olemassa varausta
        Oman varauksen muokkaaminen/poistaminen
        Omien (tai jonkun muun käyttäjän) varausten hakeminen
    Kohteiden hakeminen
        Millä perusteella haetaan: kaikki, uusimmat, tekstihaku ym
        Omien (tai jonkun muun käyttäjän) kohteiden hakeminen
        Hakufunktioita voi olla useita

Kontrollereiden riveille lisätty kommentit kullekin API-funktiolle.
