<template>
    <v-app>
        <div class="dashboard ml-5 mt-2 ">
            <v-container>
                <h1 class="grey--text heder_apb">List of Alphabets</h1>
            </v-container>
            <v-container class="card">
                <v-card flat class="pa-3" v-for="alphabet in alphabets" :key="alphabet.alphabet_id">
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

<style>

.heder_apb{
    margin-left: 8px;
    margin-top: 15px;
}

/* .heder_btn{
    margin-left: 25px;
    margin-top: 30px;
} */

.card{
    overflow: auto;
    max-height: 700px;
}

.pagefx{
    position: fixed;
}
</style>