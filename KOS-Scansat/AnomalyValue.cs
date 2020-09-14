using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kOS;
using kOS.Safe.Encapsulation;
using kOS.Safe.Encapsulation.Suffixes;
using kOS.Safe.Utilities;
using kOS.Suffixed;
using SCANsat.SCAN_Data;

namespace kOS.AddOns.kOSSCANsat
{
	[KOSNomenclature("SCANAnomaly")]
	public class AnomalyValue : kOS.Safe.Encapsulation.Structure
	{
		SCANanomaly m_anomaly;
		SharedObjects m_sharedObj;
		CelestialBody m_body;

		public AnomalyValue(SCANanomaly anomaly, CelestialBody body, SharedObjects sharedObj)
		{
			m_anomaly = anomaly;
			m_sharedObj = sharedObj;
			m_body = body;

			RegisterInitializer(InitializeSuffixes);
		}

		public override string ToString()
		{
			return Name;
		}

		private void InitializeSuffixes()
		{
			AddSuffix("DETAIL", new Suffix<BooleanValue>(() => m_anomaly.Detail));
			AddSuffix("NAME", new Suffix<StringValue>(() => Name));
			AddSuffix("GEOPOSITION", new Suffix<GeoCoordinates>(() => new GeoCoordinates(
				m_body, m_sharedObj, m_anomaly.Latitude, m_anomaly.Longitude)));
		}

		private string Name
		{
			get { return m_anomaly.Known && m_anomaly.Detail ? m_anomaly.Name : "???"; }
		}
	}
}
