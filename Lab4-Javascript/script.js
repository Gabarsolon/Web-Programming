const cities = new Map();

cities.set('BN', ['Bistrița', 'Sângeorz-Băi', 'Năsăud', 'Beclean']);
cities.set('VS', ['Bîrlad', 'Vaslui', 'Huși', 'Murgeni', 'Negrești'])
cities.set('CJ', ['Cluj-Napoca', 'Câmpia Turzii', 'Diej', 'Gherla', 'Huedin', 'Turda'])
cities.set('MS', ['Târgu Mureș', 'Iernut', 'Regin', 'Luduș'])
cities.set('IF', ['București', 'Voluntari', 'Otopeni', 'Pantelimon', 'Buftea']);

const countiesComboBox = document.getElementById("counties");
const citiesComboBox = document.getElementById('cities');

const updateCities = () => {
    //Delete any existent cities present in the combo box
    while (citiesComboBox.options.length > 0) {
        citiesComboBox.remove(0);
    }

    const selectedCounty = countiesComboBox.value;

    const selectedCountyCities = cities.get(selectedCounty);
    for (const city of selectedCountyCities){
        var cityOption = document.createElement('option');
        cityOption.text = city;
        cityOption.value = city;
        citiesComboBox.append(cityOption);
    }  
}

document.querySelector("#counties").addEventListener('change', updateCities);