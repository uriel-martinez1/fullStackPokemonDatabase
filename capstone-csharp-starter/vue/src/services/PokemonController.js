// this communicates to our Pokemon Controller on the backend
import axios from "axios";

const http = axios.create({
    baseURL: "https://localhost:44315"
});

export default {
    // lets create a new service for saving a pokemon to our favorites list
    savePokemonToFavoriteList(pokemon){
        return axios.post(`/pokemon`, pokemon);
    },
    // we need to create a service that gets all of the saved pokemon in the favorite list
    getAllFavoriteList(){
        return axios.get(`/pokemon`);
    }
}