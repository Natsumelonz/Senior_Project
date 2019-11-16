<template>
    <v-app>
        <div class="dashboard ml-5 mt-2 ">
            <v-container>
                <h1 class="subheading grey--text">List of Alphabets</h1>
                <v-card flat class="pa-3 mt-2" v-for="alphabet in alphabets" :key="alphabet.alphabet_id">
                    <v-layout row wrap>
                        <v-flex xs12 md4 class="ml-3">
                            <div class="caption grey--text ml-3">No.</div>
                            <div class="ml-3">{{alphabet.alphabet_id}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Alphabet(Japanese)</div>
                            <div>{{alphabet.alphabetname_JP}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Alphabet(Romaji)</div>
                            <div>{{alphabet.alphabetname_romanji}}</div>
                        </v-flex>
                    </v-layout>
                </v-card>
            </v-container>
        </div>
        
    </v-app>
    
</template>

<script>
import axios from "axios";

export default {
    data() { 
        return {
            alphabets: null
        }     
    },
    mounted() {
            axios
            .get("https://it59-28yomimasu.firebaseio.com/Alphabet.json").then(response =>{
                this.alphabets = response.data
            })
    }
}
</script>