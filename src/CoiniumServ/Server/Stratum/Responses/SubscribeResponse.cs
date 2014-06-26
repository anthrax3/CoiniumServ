﻿#region License
// 
//     CoiniumServ - Crypto Currency Mining Pool Server Software
//     Copyright (C) 2013 - 2014, CoiniumServ Project - http://www.coinium.org
//     https://github.com/CoiniumServ/CoiniumServ
// 
//     This software is dual-licensed: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//    
//     For the terms of this license, see licenses/gpl_v3.txt.
// 
//     Alternatively, you can license this software under a commercial
//     license or white-label it as set out in licenses/commercial.txt.
// 
#endregion
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace Coinium.Server.Stratum.Responses
{
    [JsonArray]
    public class SubscribeResponse:IEnumerable<object>
    {
        /// <summary>
        /// Used for building the coinbase.
        /// <remarks>Hex-encoded, per-connection unique string which will be used for coinbase serialization later. Keep it safe! (http://mining.bitcoin.cz/stratum-mining)</remarks>
        /// </summary>
        [JsonIgnore]   
        public string ExtraNonce1 { get; set; }

        /// <summary>
        /// The number of bytes that the miner users for its ExtraNonce2 counter 
        /// <remarks>Represents expected length of extranonce2 which will be generated by the miner. (http://mining.bitcoin.cz/stratum-mining)</remarks>
        /// </summary>
        [JsonIgnore]   
        public int ExtraNonce2Size { get; set; }

        /// <summary>
        /// The miner's subscription id.
        /// </summary>
        [JsonIgnore]
        public int SubscriptionId { get; set; }

        public IEnumerator<object> GetEnumerator()
        {
            var data = new List<object>
            {
                new List<string> // 2-tuple with name of subscribed notification and subscription ID. Teoretically it may be used for unsubscribing, but obviously miners won't use it. (http://mining.bitcoin.cz/stratum-mining)
                {
                    "mining.set_difficulty",
                    SubscriptionId.ToString(CultureInfo.InvariantCulture), // set to miner's id - just a place holder value.
                    "mining.notify", 
                    SubscriptionId.ToString(CultureInfo.InvariantCulture) // set to miner's id - just a place holder value.
                },
                ExtraNonce1, // send the ExtraNonce1
                ExtraNonce2Size // expected length of extranonce2 which will be generated by the miner
            };

            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
