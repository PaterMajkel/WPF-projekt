using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DTO;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Services
{
    public class DatabaseService
    {
        private EFDbContext _context;

        public DatabaseService()
        {
            var x = new EFDbContext.EFDbContextFactory();
            _context = x.CreateDbContext(null);
        }
        //funkcjonalność
        //dodawanie każdej z opcji prócz rangi
        //_context.Patrols.Add()
        //edytowanie linijek przekazujac obiekt i id obiektu do zmiany
        //usuwanie linijek po id

        //              zapytania do bazy
        //TODO: zbieranie podwładnych policjantów
        //TODO: zbieranie osób (przestepcow, oddzielnie wykroczenia), które złapał dany policjant (po id)
        //TODO: zbieranie przestepstw/wykroczen + filtry po atrybutach (przede wszystkim po datach)
        //TODO: zbieranie policjantów + filtr po atrybutach
        //TODO: zbieranie osób z kartoteki + filtr po atrybutach
        //TODO: zbieranie radiowozów + filtr po atrybutach
        //TODO: zbieranie patroli + filtr po atrybutach
        //TODO: komendy filtr po mieście, po strefie zagrożenia dzielnicy
        //TODO: zebranie wszystkich przestepstw/wykroczen danej osoby z kartoteki
        public ICollection<Rank> GetRanks()
        {
            return _context.Ranks.Where(p => p.IsActive).ToList();
        }
        public ICollection<PoliceCar> GetPoliceCars()
        {
            return _context.PoliceCars.Where(p => p.IsActive).ToList();
        }
        public ICollection<Crime> GetCrimes()
        {
            return _context.Crimes.Where(p => p.IsActive).ToList();
        }
        public ICollection<Felony> GetFelonys()
        {
            return _context.Felonys.Where(p => p.IsActive).ToList();
        }
        public ICollection<Policeman> GetPolicemen()
        {
            return _context.Policemans.Where(p => p.IsActive).ToList();
        }
        public ICollection<Policeman>GetPolicemenAndRank()
        {
            return _context.Policemans.Where(p => p.IsActive).Include(k => k.Rank).ToList();
        }
        public ICollection<Register> GetRegistryCoughtByPolicemanId(int id)
        {
            //do zrobienia wciaz -> przykladowe zapytanie do podpatrzenia sb
            var query = from x in _context.Registers
                        where x.RegisterId == id
                        select x;
            var y = _context.Registers.FromSqlRaw($"select * from * where id={id}");
            return query.ToList();
        }
        public ICollection<Register> GetRegisters()
        {
            var x = _context.Registers.Where(p => p.IsActive).ToList();
            return x;
        }
        public User GetUsers(string login, string password)
        {
            if (login == null || password == null)
                return null;

            var uzytkownik = _context.Users.Where(p => p.IsActive).Where(x => x.Login == login).Where(x => x.Password == password).FirstOrDefault();
            return uzytkownik;
        }
        //xD kocham SQL ;* od majkela
        public ICollection<PoliceStation> GetPoliceStations()
        {
            /* Tworzenie kwerendy
             * return _context.PoliceStations
                .Join(_context.Citys.Join(_context.Region_Citys, miasto=>miasto.ID_miasta, region=>region.ID_miasta, (miasto, region) => new { ID=region.RegionId, RegionName= region.Name, CityName = miasto.Name, Stopien_Zagrozenia = region.DangerStage })
                , komenda=>komenda.RegionId, _a=>_a.ID, (komenda, _a) => new PoliceStation_City_Region { PoliceStationId = komenda.PoliceStationId, RegionId=komenda.RegionId, RegionName=_a.RegionName, CityName=_a.CityName, DangerStage=_a.Stopien_Zagrozenia, Address=komenda.Address} )
                .ToList();*/
            return _context.PoliceStations.Where(p => p.IsActive).Include(k => k.Region_City).ThenInclude(kr => kr.City).ToList();
        }
        public void DeletePoliceStations(ICollection<PoliceStation> data)
        {
            foreach (var element in data)
            {
                //var temp = _context.PoliceStations.Find(element.PoliceStationId);
                //if (temp != null)
                //    _context.Remove(temp);
                var temp = _context.PoliceStations.Find(element.PoliceStationId);
                if (temp != null)
                {
                    temp.IsActive = false;
                }
            }
            _context.SaveChanges();
        }
        public void EditPoliceStations(PoliceStation data)
        {
            var edited = _context.PoliceStations.Where(p => p.PoliceStationId == data.PoliceStationId).FirstOrDefault();
            if (edited == null)
                return;
            edited = data;
            _context.SaveChanges();
        }

        public void DeleteCrimes(ICollection<Crime> data)
        {
            foreach (var element in data)
            {
                var temp = _context.Crimes.Find(element.PrzestepstwoId);
                if (temp != null)
                    //_context.Remove(temp);
                    temp.IsActive = false;
            }
            _context.SaveChanges();
        }
        public void DeleteFelonies(ICollection<Felony> data)
        {
            foreach (var element in data)
            {
                var temp = _context.Felonys.Find(element.FelonyId);
                if (temp != null)
                    //_context.Remove(temp);
                    temp.IsActive = false;
            }
            _context.SaveChanges();
        }
        public void DeleteRegistries(ICollection<Register> data)
        {
            foreach (var element in data)
            {
                var temp = _context.Registers.Find(element.RegisterId);
                if (temp != null)
                    //_context.Remove(temp);
                    temp.IsActive = false;
            }
            _context.SaveChanges();
        }
        public void DeletePoliceCars(ICollection<PoliceCar> data)
        {
            foreach (var element in data)
            {
                var temp = _context.PoliceCars.Find(element.PoliceCarId);
                if (temp != null)
                    //_context.Remove(temp);
                    temp.IsActive = false;

            }
            _context.SaveChanges();
        }
        public ICollection<City> GetCities()
        {
            return _context.Citys.Where(p => p.IsActive).ToList();
        }

        public ICollection<Region_City> GetRegionsOfCity(City miasto)
        {
            return _context.Region_Citys.Where(p => p.IsActive).Where(r => r.CityId == miasto.CityId).ToList();
        }

        public void AddPoliceStation(PoliceStation komenda)
        {
            _context.Add(komenda);
            _context.SaveChanges();
        }
        public void AddRegistry(Register register)
        {
            _context.Add(register);
            _context.SaveChanges();
        }
        public void EditRegistry(Register register)
        {
            var edited = _context.Registers.Where(r => r.RegisterId == register.RegisterId).FirstOrDefault();
            if (edited == null)
                return;
            _context.Registers.Remove(edited);
            _context.Registers.Add(register);
            _context.SaveChanges();
        }
        public void AddPoliceCar(PoliceCar policeCar)
        {
            _context.Add(policeCar);
            _context.SaveChanges();
        }
        public void AddCrime(Crime przestepstwo)
        {
            _context.Add(przestepstwo);
            _context.SaveChanges();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.Where(p => p.IsActive)
                .Include(p => p.Policeman).ThenInclude(p => p.PoliceStation)
                .ToList();

        }
        public void DeleteUsers(ICollection<User> data)
        {
            foreach (var element in data)
            {
                var temp = _context.Users.Find(element.UserId);
                if (temp != null && temp.Role.ToUpper() != "ADMIN")
                    //_context.Remove(temp);
                    temp.IsActive = false;

            }
        }
        public void AddUser(User uzytkownik, Policeman policjant)
        {
            if (uzytkownik == null)
                return;
            if (uzytkownik.Role.ToUpper() == "ADMIN")
            {
                uzytkownik.PolicemanId = 1;
                _context.Users.Add(uzytkownik);
            }
            else
            {
                _context.Policemans.Add(policjant);
                _context.SaveChanges();

                uzytkownik.PolicemanId = policjant.PolicemanId;
                _context.Users.Add(uzytkownik);

            }
            _context.SaveChanges();
        }
        public void AddFelony(Felony wykroczenia)
        {
            _context.Add(wykroczenia);
            _context.SaveChanges();
        }
        public void EditUser(User uzytkownik)
        {
            var edited = _context.Users.Where(p => p.UserId == uzytkownik.UserId).FirstOrDefault();
            if (edited == null)
                return;
            edited = uzytkownik;
            _context.SaveChanges();
        }
        public void EditPoliceCar(PoliceCar data)
        {
            var edited = _context.PoliceCars.Where(p => p.PoliceCarId == data.PoliceCarId).FirstOrDefault();
            if (edited == null)
                return;
            edited = data;
            _context.SaveChanges();
        }
        public Register GetRegistryByObj(Register register)
        {
            //var x =File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Police.png"));
            ////var y = Path.Combine(Directory.GetCurrentDirectory(), "Police.png");
            ;
            return _context.Registers.Where(p => p == register).Include(p => p.Felonys).Include(p => p.Crimes).First();
        }
        public Felony GetFelonyByObj(Felony wykroczenia)
        {
            return _context.Felonys.Where(p => p == wykroczenia).Include(p => p.Registers).Include(p => p.Policemans).First();
        }
        public Crime GetCrimeByObj(Crime przestepstwo)
        {
            return _context.Crimes.Where(p => p == przestepstwo).Include(p => p.Registers).Include(p => p.Policemans).First();
        }
        public ICollection<Region_City> GetRegions()
        {
            return _context.Region_Citys.Where(p => p.IsActive).Include(p => p.City).ToList();
        }
        public void EditRegion(Region_City data)
        {
            var edited = _context.Region_Citys.Where(p => p.Region_CityId == data.Region_CityId).FirstOrDefault();
            if (edited == null)
                return;
            edited = data;
            _context.SaveChanges();
        }
        public void AddRegistryToCrime(Crime przestepstwo, Register register)
        {
            var temp = _context.Crimes.Where(p => p.IsActive).Where(p => p.PrzestepstwoId == przestepstwo.PrzestepstwoId).Include(p => p.Registers).First();
            if (temp != null)
            {
                temp.Registers.Add(register);
                _context.SaveChanges();
            }
        }
         public void DeletePolicemenFromCrime(Crime data, ICollection<Policeman> police)
        {
            var temp = _context.Crimes.Where(p=> p.IsActive && p.PrzestepstwoId==data.PrzestepstwoId).Include(p=>p.Policemans).First();
            foreach (var element in police)
            {
                if (element != null)
                    temp.Policemans.Remove(element);
            }
            _context.SaveChanges();
        }
     
        public void DeleteFelonFromCrime(Crime data, ICollection<Register> przestepca)
        {
            var temp = _context.Crimes.Where(p => p.IsActive && p.PrzestepstwoId == data.PrzestepstwoId).Include(p => p.Registers).First();
            foreach (var element in przestepca)
            {
                if (element != null)
                    temp.Registers.Remove(element);
            }
            _context.SaveChanges();
        }
        public void DeletePolicemenFromFelony(Felony data, ICollection<Policeman> police)
        {
            var temp = _context.Felonys.Where(p => p.IsActive && p.FelonyId == data.FelonyId).Include(p => p.Policemans).First();
            foreach (var element in police)
            {
                if (element != null)
                    temp.Policemans.Remove(element);
            }
            _context.SaveChanges();
        }
        public void DeleteFelonFromFelony(Felony data, ICollection<Register> przestepca)
        {
            var temp = _context.Felonys.Where(p => p.IsActive && p.FelonyId == data.FelonyId).Include(p => p.Registers).First();
            foreach (var element in przestepca)
            {
                if (element != null)
                    temp.Registers.Remove(element);
            }
            _context.SaveChanges();
        }

        public User GetUserByObj(User data)
        {
            if (data != null)
            {
                return _context.Users.Where(p => p.IsActive && p.PolicemanId == data.PolicemanId).Include(p => p.Policeman).ThenInclude(p => p.Patrols).ThenInclude(p => p.PoliceCar).First();
            }
            return null;
        }
        public void AddPolicemanToCrime(Crime przestepstwo, Policeman policjant)
        {
            var temp = _context.Crimes.Where(p => p.IsActive).Where(p => p.PrzestepstwoId == przestepstwo.PrzestepstwoId).Include(p => p.Policemans).First();
            if (temp != null)
            {
                temp.Policemans.Add(policjant);
                _context.SaveChanges();
            }
        }
        public void AddRegistryToFelony(Felony wykroczenia, Register register)
        {
            var temp = _context.Felonys.Where(p => p.IsActive).Where(p => p.FelonyId == wykroczenia.FelonyId).Include(p => p.Registers).First();
            if (temp != null)
            {
                temp.Registers.Add(register);
                _context.SaveChanges();
            }
        }
        public void AddPolicemanToFelony(Felony wykroczenia, Policeman policjant)
        {
            var temp = _context.Felonys.Where(p => p.IsActive).Where(p => p.FelonyId == wykroczenia.FelonyId).Include(p => p.Policemans).First();
            if (temp != null)
            {
                temp.Policemans.Add(policjant);
                _context.SaveChanges();
            }

        }
        public ICollection<Policeman> GetSubordinates(Policeman policjant)
        {
            return _context.Policemans.Where(p=>p.IsActive).Where(p=>p.PoliceStationId==policjant.PoliceStationId && p.RankId< policjant.RankId).Include(p=>p.Rank).Include(p => p.PoliceStation).ToList();
        }
        public void AddPatrol(Patrol patrol)
        {
            _context.Patrols.Add(patrol);
            _context.SaveChanges();

        }
        public Policeman GetPolicemanByObj(Policeman data)
        {
            if (data != null)
            {
                return _context.Policemans.Where(p => p.IsActive && p.PolicemanId == data.PolicemanId).Include(p => p.Patrols).ThenInclude(p => p.PoliceCar).First();
            }
            return null;
        }
    }
}
