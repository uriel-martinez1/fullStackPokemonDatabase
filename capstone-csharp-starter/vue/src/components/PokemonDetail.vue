<template>
    <div>
        <h1>Pokemon Detail for {{ pokemon.name }}</h1>
        <p>Height: {{ pokemon.height }}  Weight: {{ pokemon.weight }}</p>
        <img :src="frontImg" />
        <br>
        <img :src="backImg" alt="">

        <!--Added a button to save pokemon on list-->
        <button v-on:click="saveToFavoriteList()">Save to Favorites</button>

        <!--Popup message for success-->
        <div class="popup success" v-if="showSuccessMessage">
            Pokemon added to favorites successfuly!
        </div>

        <!--Popup message for error-->
        <div class="popup error" v-if="showErrorMessage">
            Unable to add Pokemon to favorites. Please try again.
        </div>

    </div>
</template>

<script>
import service from '../services/PokemonService';
import PokemonControllerService from '../services/PokemonController'

export default {
    props: {
        id: String
    },
    data (){
        return {
            pokemon: {},
            frontImg: "",
            backImg: "",
            pokemonDetail: {},

            //trigger for showing success and error messages
            showSuccessMessage: false,
            showErrorMessage: false
        }
    },
    created() {
        service.getPokemonDetail(this.id)
        .then ((response) => {
            this.pokemon = response.data;
            this.frontImg = response.data.sprites.front_default;
            this.backImg = response.data.sprites.back_default;

            this.pokemonDetail = {
                apiId: this.pokemon.id,
                baseExperience: this.pokemon.base_experience,
                height: this.pokemon.height,
                weight: this.pokemon.weight,
                species: {
                    name: this.pokemon.species.name,
                    url: this.pokemon.species.url
                },
                sprites: {
                    back_default: this.pokemon.sprites.back_default,
                    front_default: this.pokemon.sprites.front_default
                }
            }
        });
    }, 
    methods: {
        saveToFavoriteList(){
            // call the pokemon service
            PokemonControllerService.savePokemonToFavoriteList(this.pokemonDetail)
            .then((response) => {
                // if the request was able to send properly
                if (response.status === 201){
                    // lets create a simple pop up on the screen that says it was added successfully
                    this.showSuccessMessage = true;
                    this.showErrorMessage = false;

                    // Hide message after 3 seconds
                    setTimeout(() => {
                        this.showSuccessMessage = false;
                    }, 3000);
                }
            })
            .catch((error) => {
                // lets send a message to user that something happened
                this.showSuccessMessage = false;
                this.showErrorMessage = true;

                // Hide message after 3 seconds
                    setTimeout(() => {
                        this.showErrorMessage = false;
                    }, 3000);
            });
        },
        // This is how it works in Java
        saveFavorite(){
            PokemonControllerService.savePokemonToFavoriteList(this.pokemon)
            .then((response) => {
                //console.log(response);
                alert('Pokemon was saved to favorites!');
            })
            // this is for catching the error message from the backend
            .catch((error) =>{
                //console.log(error.response);
                if(error.response){
                    alert('Unable to save to favorites!\n ' + error.response.data.message);
                }
            })
        },
    }
}
</script>

<style scoped>
img {
    width: 200px;
    height: auto;
}

.success {
    background-color: green;
}

.error {
    background-color: lightcoral;
}

</style>