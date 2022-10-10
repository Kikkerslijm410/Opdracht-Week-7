Feature: Attractie

Scenario: BestaatAl
    Given attractie Draaimolen bestaat
    When attractie Draaimolen wordt toegevoegd
    Then moet er een error 403 komen

# Opdracht: voeg hier een scenario toe waarin een 404 wordt verwacht bij het deleten van een niet-bestaande attractie