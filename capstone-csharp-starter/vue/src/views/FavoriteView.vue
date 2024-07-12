<template>
    <div>
        <h1>List of Favorite Pokemon:</h1>
        <div class="pokeList">
            <router-link v-for="pokemon in pokemonArray" v-bind:key="pokemon.id"
                :to="{ name: 'detail', params: { id: pokemon.id } }">
                <div class="poke"> {{ pokemon.species.name }}</div>
            </router-link>
        </div>
    </div>
</template>

<script>
import PokemonController from '../services/PokemonController';

export default {
    data() {
        return {
            pokemonArray: [],
        }
    }, 
    // lifecycle hook 
    created() {
        PokemonController.getAllFavoriteList() // async communication
        .then((response) => {
            //console.log(response);
            this.pokemonArray = response.data;
        })
    }
}
</script>

<style>
</style>