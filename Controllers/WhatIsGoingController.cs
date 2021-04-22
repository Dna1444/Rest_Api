using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BuildingApi.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

namespace BuildingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhatIsGoingController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;

        public WhatIsGoingController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        // GET: api/whatIsGoing
        [HttpGet]
        public string GetWhatIsGoing(){
            //collecting the information in different variables
            int amountOfElevators = _context.Elevators.ToList().Count();
            int amountOfBatteries = _context.Batteries.ToList().Count();
            int amountOfCustomers = _context.Customers.ToList().Count();
            int amountOfQuotes = _context.Quotes.ToList().Count();
            int amountOfLeads = _context.Leads.ToList().Count();
            var listElevatorsNotRunning = from elevator in _context.Elevators where elevator.Status.ToLower() != "Online" select elevator;
            int amountOfElevatorsNotRunning = listElevatorsNotRunning.Count();
            IEnumerable<Addresses> buildingAddresses = from address in _context.Addresses where address.City !="" select address;
            int amountOfBuildings = buildingAddresses.Select(building => building.NumberStreet).Distinct().Count();
            int amountOfCities = buildingAddresses.Select(building => building.City).Distinct().Count();
            Console.WriteLine(listElevatorsNotRunning);
            //Answear structure
            string answear = "Greetings,\nThere are currently "+ amountOfElevators +" elevators deployed in "+ amountOfBuildings +" buildings of your " + amountOfCustomers +" customers.\nCurrently, " + amountOfElevatorsNotRunning +" elevators are not in Running Status and are being serviced. \n" + amountOfBatteries + " Batteries are deployed across "+ amountOfCities +" cities. \nOn another note you currently have "+ amountOfQuotes+" quotes awaiting processing. \nYou also have "+ amountOfLeads+ " leads in your contact requests";
            return answear;
        }
       
        [HttpGet("fr")]
        public string GetWhatIsGoingFr(){
            //collecting the information in different variables
            int amountOfElevators = _context.Elevators.ToList().Count();
            int amountOfBatteries = _context.Batteries.ToList().Count();
            int amountOfCustomers = _context.Customers.ToList().Count();
            int amountOfQuotes = _context.Quotes.ToList().Count();
            int amountOfLeads = _context.Leads.ToList().Count();
            var listElevatorsNotRunning = from elevator in _context.Elevators where elevator.Status.ToLower() != "Online" select elevator;
            int amountOfElevatorsNotRunning = listElevatorsNotRunning.Count();
            IEnumerable<Addresses> buildingAddresses = from address in _context.Addresses where address.City !="" select address;
            int amountOfBuildings = buildingAddresses.Select(building => building.NumberStreet).Distinct().Count();
            int amountOfCities = buildingAddresses.Select(building => building.City).Distinct().Count();
            Console.WriteLine(listElevatorsNotRunning);
            //Answear structure
            string answear = "Bonjour, il y a présentement "+ amountOfElevators +" ascenseurs déployés dans "+ amountOfBuildings +" bâtiments de " + amountOfCustomers +" clients. Il y a présentement  " + amountOfElevatorsNotRunning +" ascenseur qui ne sont pas en fonction et sont en cours de maintenance. Également, " + amountOfBatteries + " batteries sont déployés dans plus de "+ amountOfCities +" villes. Sur une autre note, vous avez actuellement "+ amountOfLeads+" soumissions en attente de traitement. Vous avez également "+ amountOfQuotes+ " requêtes dans vos demandes de contact";
            return answear;
        }
       

    }
}