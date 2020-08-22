using GeoCoordinatePortable;
using System;

namespace Devboost.DroneDelivery.DomainService
{
    public class GeolocalizacaoService
    {
		public double CalcularDistanciaEmMetro(double latitudeInicial,double longitudeInicial, double latitudeFinal, double longitudeFinal)
		{
			GeoCoordinate saidaCoordenada = new GeoCoordinate();
			saidaCoordenada.Latitude = latitudeInicial;
			saidaCoordenada.Longitude = longitudeInicial;

			GeoCoordinate entradaCordenada = new GeoCoordinate();
			entradaCordenada.Latitude = latitudeFinal;
			entradaCordenada.Longitude = longitudeFinal;

			double distancia = saidaCoordenada.GetDistanceTo(entradaCordenada);
			return distancia;
		}
	}
}
