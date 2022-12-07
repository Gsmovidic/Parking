using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Enums;
using Parking.Models;
using Vereyon.Web;

namespace Parking.Controllers
{
    public class EntradasController : Controller
    {

        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public EntradasController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        [HttpGet]
        public async Task<IActionResult> Entrada()
        {
            return View();
        }

       [HttpPost]

        public async Task<IActionResult> Entrada(EntradaVM entradaVM) 
        {
                string pl = entradaVM.Placa;
                if (pl.Length != 6)
                {
                    _flashMessage.Danger("Por favor ingrese una placa Valida!");
                }
                else
                {                 
                    VehiculosController vehiculoC = new VehiculosController(); //Controlador necesario para validar la placa
                    if (vehiculoC.ValidarPlaca(pl))
                    {
                        RegistroEntrada regisEntrada = await _context.RegistroEntradas.FirstOrDefaultAsync(r => !r.Pago && r.PlacaVehiculo == pl);
                        if (regisEntrada == null)  //Linea para poder validar que el operario no ingrese la misma placa dos veces y sepa que este vehiculo ya se encuentra dentro del parqueadero 
                        {
                            TipoVehiculo tV = vehiculoC.ValidarTipoVehiculo(pl);

                            Vehiculo vehi = await _context.Vehiculos.Include(v=>v.Cliente).FirstOrDefaultAsync(v => v.Placa == pl); //Consulto si el vehiculo se encuentra registrado en el sistema , recordar que no todos los vehiculos que vayan a ingresar al parqueadero estan registrado
                            Parqueadero park = await _context.Parqueaderos.FirstOrDefaultAsync(p => p.Id == 1); //consulto el unico parqueadero que hay en el momento para poder manejar la clase 
                            ParqueaderoController parkC = new ParqueaderoController(_context); // para poder validar las otras cosas necesarias
                            Celda cel = await _context.Celdas.FirstOrDefaultAsync(c => c.TipoVehiculoCelda == tV && c.Disponible);
                            ClientePlan cP = null;
                            //consulto un plan asociado al cliente traido por el id en vehiculo, luego que el plan concuerde con el tipo de vehiculo a ingresar, que el plan aun siga vigente y no se este usando en el momento 
                            if (vehi != null)
                            {
                                 cP = await _context.ClientesPlanes.FirstOrDefaultAsync(c => c.Cliente == vehi.Cliente && c.TipoVehiculo == tV && !c.EnUso && c.Vigente);
                                if (cP != null)
                                {
                                    ClientePlanController cPlanC = new ClientePlanController(_context);
                                    RegistroEntrada en = new RegistroEntrada
                                    {
                                        FechaHoraEntrada = DateTime.Now,
                                        PlacaVehiculo = pl,
                                        TipoCliente = TipoCliente.Plan,
                                        Parqueadero = park
                                    };
                                    _context.RegistroEntradas.Add(en);
                                    await cPlanC.ActualizarEnuso(cP, 1); //1 es para ingreso de vehiculos y saber como asignar la variable enUso
                                    _flashMessage.Confirmation($"Se ha permitido acceso al vehiculo: {pl}"!);
                                }
                              
                            }

                            if(vehi==null || vehi!=null && cP==null ) 
                            {
                                if (parkC.ValidarDisponibilidadCeldas(park, tV))
                                {
                                    RegistroEntrada en = new RegistroEntrada
                                    {
                                        FechaHoraEntrada = DateTime.Now,
                                        PlacaVehiculo = pl,
                                        TipoCliente = TipoCliente.Ocasional,
                                        Parqueadero = park
                                    };
                                    _context.RegistroEntradas.Add(en);
                                    cel.PlacaVehiculo = pl;
                                    cel.Disponible = false;
                                    _context.Update(cel);
                                    await parkC.ActualizarNumeroCeldas(park, tV);
                                    //comente esta linea debido a que en el metodo de arriba yo actualizo el número de celdas y ya utilzo el savechanges asi que supongo que no debo llamarlo otra vez 
                                    _flashMessage.Confirmation($"Se ha permitido acceso al vehiculo: {pl}"! );
                                }

                                else
                                {
                                    _flashMessage.Warning("En el momento el parqueadero no cuenta con celdad disponibles!");
                                }

                  
                            }
                        }
                        else
                        {
                            _flashMessage.Danger($"El vehiculo {pl} ya se encuentra en el parqueadero" );
                        }

                    }
                    else
                    {
                        _flashMessage.Danger("Por favor ingresar una placa valida");
                    }


                }
            
           return RedirectToAction();

        }




    }
}
