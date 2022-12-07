using Microsoft.EntityFrameworkCore;
using Parking.Data.Entities;
using Parking.Enums;

namespace Parking.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;


        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync() 
        {
            await _context.Database.EnsureCreatedAsync();
            await anadirParqueadero();
            await anadirPlanes();
            await anadirClientesPlanes();
         
        }

        private async Task anadirParqueadero() 
        {
            if (!_context.Parqueaderos.Any()) 
            {
                Parqueadero park = new Parqueadero { Ubicacion="Calle 59 #85-101" };

                List<Celda> celdas = new List<Celda>();
                for (int i = 0; i < 100; i++)
                {
                    if (i < 50)
                    {
                        Celda c = new Celda
                        {
                            Parqueadero = park,
                            Disponible = true,
                            Ubicación = "Primer Piso",
                            TipoVehiculoCelda = TipoVehiculo.Carro

                        };
                        _context.Celdas.Add(c);
                        celdas.Add(c);
                    }
                    else 
                    {
                        Celda c = new Celda
                        {
                            Parqueadero = park,
                            Disponible = true,
                            Ubicación = "Segundo Piso",
                            TipoVehiculoCelda = TipoVehiculo.Moto
                        };
                        _context.Celdas.Add(c);
                        celdas.Add(c);
                    }    
                }
                park.Celdas = celdas;
                park.NroCeldasMoto = park.Celdas.Count(c => c.TipoVehiculoCelda == TipoVehiculo.Moto);
                park.NroCeldasCarro = park.Celdas.Count(c => c.TipoVehiculoCelda == TipoVehiculo.Carro);
                _context.Add(park);
                await  _context.SaveChangesAsync();
            }
        }

        private async Task anadirPlanes() 
        {
            if (!_context.Planes.Any())
            {
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Mensualidad Carro", TipoVehiculo = TipoVehiculo.Carro, ValorPlan = 80000 });
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Quincena Carro", TipoVehiculo = TipoVehiculo.Carro, ValorPlan = 60000 });
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Dia Carro", TipoVehiculo = TipoVehiculo.Carro, ValorPlan = 8000 });
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Mensualidad Moto", TipoVehiculo = TipoVehiculo.Moto, ValorPlan = 50000 });
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Quincena  Moto", TipoVehiculo = TipoVehiculo.Moto, ValorPlan = 30000 });
                _context.Planes.Add(new Plan { Disponibilidad = true, Nombre = "Dia Moto", TipoVehiculo = TipoVehiculo.Moto, ValorPlan = 6000 });
                await _context.SaveChangesAsync();
            }
            
        }

        private async Task anadirClientesPlanes()
        {
            if (!_context.ClientesPlanes.Any())
            {
                //Añadir clientes primero 
                Cliente c1 = new Cliente { Cedula = "1000902511", Correo = "victorzgames00@gmail.com", Nombre = "Victor Manuel Tobon" };
                Cliente c2 = new Cliente { Cedula = "43758113", Correo = "ale20161977@gmail.com", Nombre = "Alejandra Sepulveda" };
                Cliente c3 = new Cliente { Cedula = "1000539403", Correo = "anamaria@gmail.com", Nombre = "Ana Maria Valencia" };
                _context.Add(c1);
                _context.Add(c2);
                _context.Add(c3);

                // consulto parqueadero existente 
                Parqueadero park = await _context.Parqueaderos.FirstOrDefaultAsync(p => p.Id == 1);
                //Asocie vehiculos a un cliente 
                Vehiculo ve1 = new Vehiculo { Color = "Rojo", Placa = "ABC123", TipoVehiculo = TipoVehiculo.Carro, Parqueadero = park, Cliente = c1 };
                Vehiculo ve2 = new Vehiculo { Color = "Azul", Placa = "ABC12E", TipoVehiculo = TipoVehiculo.Moto, Parqueadero = park, Cliente = c2 };
                Vehiculo ve3 = new Vehiculo { Color = "Blanco", Placa = "ABC124", TipoVehiculo = TipoVehiculo.Carro, Parqueadero = park, Cliente = c3 };
                Vehiculo ve4 = new Vehiculo { Color = "Rojo", Placa = "HPB28F", TipoVehiculo = TipoVehiculo.Moto, Parqueadero = park, Cliente = c3 };
                Vehiculo ve5 = new Vehiculo { Color = "Beige", Placa = "HPE403", TipoVehiculo=TipoVehiculo.Carro , Parqueadero= park, Cliente=c3 } ;
                _context.Add(ve1);
                _context.Add(ve2);
                _context.Add(ve3);
                _context.Add(ve4);
                _context.Add(ve5);

                //Hora de crear clientes con planes 
                Plan p1 = await _context.Planes.FirstOrDefaultAsync(p => p.Id == 1);  //mensualidad carro
                Plan p2 = await _context.Planes.FirstOrDefaultAsync(p => p.Id == 4); //mensualidad moto

                ClientePlan cP = new ClientePlan { Cliente = c3, Plan = p2, FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(30), EnUso = false, Vigente = true, TipoVehiculo = TipoVehiculo.Moto };
                ClientePlan cP2 = new ClientePlan { Cliente = c3, Plan = p1, FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(30), EnUso = false, Vigente = true, TipoVehiculo = TipoVehiculo.Carro };
                _context.Add(cP);
                _context.Add(cP2);
                await _context.SaveChangesAsync();
            }


        }


    }
}
