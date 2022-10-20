Feature: Attractie

Scenario: AttractieBestaatAl
    Given attractie Draaimolen bestaat
    When attractie Draaimolen wordt toegevoegd
    Then moet er een error 403 komen

Scenario: AttractieBestaatNietEnWordtVerwijderd
    Given attractie Zweefmolen bestaat niet
    When attractie Zweefmolen wordt verwijderd
    Then moet er een code 404 komen

Scenario: AttractieWordtSuccessvolToegevoegd
    Given attractie Achtbaan bestaat niet
    When attractie Achtbaan wordt toegevoegd
    Then moet er een code 201 komen

 Scenario: GastBestaatNietEnWordtVerwijderd
    Given gast Lucas bestaat niet
    When gast Lucas wordt verwijderd
    Then moet er een foutcode 404 komen
