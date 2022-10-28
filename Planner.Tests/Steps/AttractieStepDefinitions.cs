using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Xunit;
using TechTalk.SpecFlow;
using Planner.Tests.Hooks;
using Planner.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Planner.Tests.Steps;

[Binding]
public sealed class AttractieStepDefinitions
{
    private readonly RestClient _client;
    private readonly DatabaseData _databaseData;
    private RestResponse? response;

    public AttractieStepDefinitions(DatabaseData databaseData){
        _databaseData = databaseData;
        _client = new RestClient("http://localhost:5001/");

        // Het HTTPS certificaat hoeft niet gevalideerd te worden, dus return true
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
    }

    //Test 1 AttractieBestaat
    [Given("attractie (.*) bestaat")]
    public async Task AttractieBestaat(string naam){
        await _databaseData.Context.Attractie.AddAsync(new Attractie { Naam = naam });
        await _databaseData.Context.SaveChangesAsync();
    }
    [When("attractie (.*) wordt toegevoegd")]
    public async Task AttractieToevoegen(string naam){
        var request = new RestRequest("api/Attracties").AddJsonBody(new { Naam = naam, Reserveringen = new List<string>() });
        response = await _client.ExecutePostAsync(request);
    }
    [Then("moet er een error (.*) komen")]
    public void Error(int httpCode){
        Assert.Equal(httpCode, (int)response!.StatusCode);
    }
    //Einde test 1

    //Test 2 AttractieBestaatNietEnWordtVerwijderd
    [Given("attractie (.*) bestaat niet")]
    public async Task AttractieBestaatNiet(string naam){
        var lijst = await _databaseData.Context.Attractie.ToArrayAsync();
        for(int i=0; i< await _databaseData.Context.Attractie.CountAsync<Attractie>(); i++){
            if(lijst[i].Naam == naam){
                _databaseData.Context.Attractie.Remove(lijst[i]);
            }
        }
    }
    [When("attractie (.*) wordt verwijderd")]
    public async Task AttractieDelete(string naam){
        var lijst = await _databaseData.Context.Attractie.ToArrayAsync();
        for(int i=0; i< await _databaseData.Context.Attractie.CountAsync<Attractie>(); i++){
            if(lijst[i].Naam == naam){
                var request = new RestRequest("api/Attracties/"+lijst[i].Id);
                response = await _client.DeleteAsync(request);
            }
        }
        response = await _client.DeleteAsync(new RestRequest("api/Attracties/"+ await _databaseData.Context.Attractie.CountAsync()+1));
    }
    [Then("moet er een code (.*) komen")]
    public void Code(int httpCode){
        Assert.Equal(httpCode, (int)response!.StatusCode);
    }
    //Einde Test 2

    //Test 3 AttractieWordtSuccessvolToegevoegd
    [Given("attractie (.*) bestaat nog niet")]
    public async Task AttractieBestaatNietCheck(string naam){
        var lijst = await _databaseData.Context.Attractie.ToArrayAsync();
        for(int i=0; i< await _databaseData.Context.Attractie.CountAsync<Attractie>(); i++){
            if(lijst[i].Naam == naam){
                _databaseData.Context.Attractie.Remove(lijst[i]);
            }
        }
    }
    [When("attractie (.*) wordt aangemaakt")]
    public async Task AttractieToevoegen2(string naam){
        var request = new RestRequest("api/Attracties").AddJsonBody(new { Naam = naam, Reserveringen = new List<string>() });
        response = await _client.ExecutePostAsync(request);
    }
    [Then("moet de code (.*) komen")]
    public void ControleCode(int httpCode){
        Assert.Equal(httpCode, (int)response!.StatusCode);
    }
    //Einde Test 3

    //Test 4 GastBestaatNietEnWordtVerwijderd
    [Given("gast (.*) bestaat niet")]
    public async Task GastBestaatNiet(string naam){
        var lijst = await _databaseData.Context.Gast.ToArrayAsync();
        for(int i=0; i< await _databaseData.Context.Gast.CountAsync<Gast>(); i++){
            if(lijst[i].Naam == naam){
                _databaseData.Context.Gast.Remove(lijst[i]);
            }
        }
    }
    [When("gast (.*) wordt verwijderd")]
    public async Task GastDelete(string naam){
        var lijst = await _databaseData.Context.Gast.ToArrayAsync();
        for(int i=0; i< await _databaseData.Context.Gast.CountAsync<Gast>(); i++){
            if(lijst[i].Naam == naam){
                var request = new RestRequest("api/GastenController/"+lijst[i].Id);
                response = await _client.DeleteAsync(request);
            }
        }
        response = await _client.DeleteAsync(new RestRequest("api/GastenController/"+ await _databaseData.Context.Gast.CountAsync()+1));
    }
    [Then("moet er een foutcode (.*) komen")]
    public void FoutCode(int httpCode){
        Assert.Equal(httpCode, (int)response!.StatusCode);
    }  
    //Einde Test 4
}

class AttractieToegevoegd
{
    public int Id { get; set; }
}