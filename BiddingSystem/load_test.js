import { check, sleep } from 'k6'; // Import check module for validating responses
import http from 'k6/http'; // Import k6 http module

// Define the API URL for your PlaceBid endpoint
const API_URL = 'http://localhost:5111/bids'; // Update this with your actual API URL

// Function to generate random data for the PlaceBidCommand
function generateRandomBid() {
    const amount = Math.floor(Math.random() * 1000) + 1; // Random amount between 1 and 1000
    const itemId = `item_${Math.floor(Math.random() * 100)}`; // Random item ID
    const bidderId = `bidder_${Math.floor(Math.random() * 1000)}`; // Random bidder ID
    
    return {
        Amount: amount,
        ItemId: itemId,
        BidderId: bidderId,
    };
}

// Define the default function for k6 to simulate virtual users
export default function () {
    // Generate random bid data
    const bidData = generateRandomBid();

    // Send a POST request to the /bids endpoint
    const res = http.post(API_URL, JSON.stringify(bidData), {
        headers: { 'Content-Type': 'application/json' },
    });

    // Check if the response status is 202 Accepted
    check(res, {
        'status is 202': (r) => r.status === 202,
    });

    // Sleep for a random time between 1 and 3 seconds to simulate think time
    sleep(Math.random() * 2 + 1); // Sleep between 1 and 3 seconds
}

// Define the load testing scenario
export let options = {
    // Define a scenario where we simulate 50 virtual users (VUs) ramping up from 0 to 50 VUs over 1 minute, running for 10 minutes.
    scenarios: {
        loadTest: {
            executor: 'ramping-vus',
            startVUs: 0, // start with 0 VUs
            stages: [
                { duration: '1m', target: 50 }, // Ramp up to 50 VUs in 1 minute
                { duration: '8m', target: 50 }, // Maintain 50 VUs for 8 minutes
                { duration: '1m', target: 0 }, // Ramp down to 0 VUs in 1 minute
            ],
        },
    },
};
