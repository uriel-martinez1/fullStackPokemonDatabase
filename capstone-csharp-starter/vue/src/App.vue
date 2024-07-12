<template>
  <div id="capstone-app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      <!--This will show only if the user is logged in-->
      <span v-if="$store.state.token != ''">&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }">Logout</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'pokemon' }">View Pokemon</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{name: 'favorite'}">View Favorite Pokemon</router-link>
      </span>
    </div>
    <router-view />
  </div>
</template>
