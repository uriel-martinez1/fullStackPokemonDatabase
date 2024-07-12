<template>
    <pokemon-detail v-bind:id="$route.params.id"></pokemon-detail>
</template>

<script>
import PokemonDetail from '../components/PokemonDetail.vue';

export default {
    components: { PokemonDetail}
}
</script>

<style>
</style>