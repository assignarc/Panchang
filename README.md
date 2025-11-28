# Panchang

> A comprehensive C# library for calculating the Hindu Panchang (five-limb calendar) and Vedic astrology computations with high accuracy and performance.

[![License: GPL v2](https://img.shields.io/badge/License-GPL%20v2-blue.svg)](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html)
[![.NET](https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)

## Overview

Panchang is a powerful computational library designed to calculate the five essential aspects (Panch-Ang) of the Hindu calendar system. Built with precision and performance in mind, it leverages the renowned Swiss Ephemeris for accurate planetary positions and provides real-time calculations suitable for Vedic astrology, Muhurta selection, and calendar event tracking.

## Features

### Core Panchang Calculations

The library computes all five limbs of the Hindu daily calendar:

- **Vara (Weekday)**: Day of the week, each associated with a ruling planet
- **Tithi (Lunar Day)**: Lunar phase determined by the angular relationship between the Moon and Sun
- **Nakshatra**: Lunar mansion or constellation position of the Moon
- **Yoga**: Calculated from the sum of the Sun's and Moon's longitudes, divided into 27 parts
- **Karana**: Half of a Tithi, based on the angular difference between the Sun and Moon

### Advanced Capabilities

- **Location-Aware Calculations**: Luni-solar calendar computations adjusted for geographic position and altitude
- **Muhurta Determination**: Calculate auspicious timings for important events
- **Hindu Event Tracking**: Link and identify significant dates including:
  - Ekadashi names
  - Amavasya (new moon) observances
  - Prominent Tithis
  - Jayanti (birth anniversaries) and Punyatithi (death anniversaries)
  - Festivals and celebrations (Jatra/Mela/Utsav)
- **Vedic Astrology Support**: Comprehensive tools for astrological calculations
- **High Performance**: Optimized for real-time calculations with superior speed and accuracy
- **Swiss Ephemeris Integration**: Leverages SwissEphNet for precise planetary position calculations

## Project Structure

```
Panchang/
├── Panchang/              # WinForms UI and application layer
├── PanchangLib/           # Core calculation library
├── PanchangTest/          # Unit and integration tests
├── PanchangTestConsole/   # Console application for testing
└── Archived/              # Legacy components
```

## Installation

### Prerequisites

- .NET Framework or .NET Core/.NET 5+
- Visual Studio 2019 or later (recommended)

### Building from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Panchang.git
   cd Panchang
   ```

2. Open the solution:
   ```bash
   open Panchang.sln
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

## Usage

### Basic Example

```csharp
using PanchangLib;

// Create a Horoscope instance for a specific location and time
var location = new Location(latitude, longitude, altitude);
var dateTime = new DateTime(2025, 11, 27, 18, 0, 0);
var horoscope = new Horoscope(dateTime, location);

// Calculate Panchang
var panchang = new HinduPanchang(horoscope);

// Access the five limbs
var vara = panchang.GetVara();
var tithi = panchang.GetTithi();
var nakshatra = panchang.GetNakshatra();
var yoga = panchang.GetYoga();
var karana = panchang.GetKarana();
```

## Roadmap

### Current Development

- [ ] Expand calendar event database (festivals, observances)
- [ ] Add multi-lingual support for wider accessibility
- [ ] Incorporate regional variations of Hindu calendars across India
- [ ] Enhance test suite coverage
- [ ] API documentation

### Future Enhancements

- RESTful API for web integration
- Mobile app support
- Enhanced visualization tools

## Contributing

We welcome contributions from the community! Please follow these guidelines:

1. **Fork** the repository
2. Create a **feature branch** (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add some amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. Open a **Pull Request**

### Development Guidelines

- Please do **not** push directly to the `master` branch
- Add appropriate **test cases** for new features
- Follow existing code style and conventions
- Update documentation as needed

## Testing

Run the test suite to ensure accuracy and reliability:

```bash
dotnet test
```

## License

This program is free software; you can redistribute it and/or modify it under the terms of the **GNU General Public License** as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but **WITHOUT ANY WARRANTY**; without even the implied warranty of **MERCHANTABILITY** or **FITNESS FOR A PARTICULAR PURPOSE**. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.

## Acknowledgments

This project builds upon the excellent work of several open-source projects:

- **mhora** (v0.2) - Vedic Astrology software under GNU GPL
- **Genghis Integrated WinForms Components** - By Genghis Group (Copyright © 2002-2004)
- **[Swiss Ephemeris](https://www.astro.com/swisseph/)** - Under GNU Affero General Public License (AGPL)
- **[SwissEphNet](https://github.com/ygrenier/SwissEphNet)** - C# port of Swiss Ephemeris (v2.06) for cross-platform usage

## Contact & Support

For questions, suggestions, or issues, please:
- Open an [issue](https://github.com/yourusername/Panchang/issues)
- Submit a [pull request](https://github.com/yourusername/Panchang/pulls)

---

**Made with ❤️ for the preservation and advancement of Vedic astronomical knowledge**
