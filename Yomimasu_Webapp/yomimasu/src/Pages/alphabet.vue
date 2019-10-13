<template>
    <v-app>
        <div class="dashboard ml-5 mt-2 ">
            <h1 class="subheading grey--text">Alphabet</h1>
            <v-container>
                <v-card flat class="pa-3" v-for="alphabet in alphabets" :key="alphabet.number">
                    <v-layout row wrap>
                        <v-flex xs12 md4 class="ml-3">
                            <div class="caption grey--text">No.</div>
                            <div>{{alphabet.number}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Alphabet(Japanese)</div>
                            <div>{{alphabet.alphabetJP}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Alphabet(Romaji)</div>
                            <div>{{alphabet.alphabetRM}}</div>
                        </v-flex>
                    </v-layout>
                </v-card>
            </v-container>
        </div>
        
    </v-app>
    
</template>

<script>

export default {
    data() { 
        return {
            alphabets: [
            { number: 1, alphabetJP: "あ", alphabetRM: 'a' },
            { number: 2, alphabetJP: "い", alphabetRM: 'i' },
            { number: 3, alphabetJP: "う", alphabetRM: 'u' }
            ]
        }     
    },
 
 
}
</script>