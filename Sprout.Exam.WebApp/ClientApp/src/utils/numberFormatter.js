const NumberFormatter = () => {

  const commaTwoDecimalString = (number) => {
    if (isNaN(Number(number))) { return number };
    
    return number.toLocaleString('en-US', { 
      style: 'decimal', 
      minimumFractionDigits: 2, 
      maximumFractionDigits: 2 
    });
  }
  
  const stringToNumTwoDecimal = (numberString) => {
    if (isNaN(Number(numberString))) { return numberString; };
    
    return Number(numberString).toFixed(2);
  }

  return {
    commaTwoDecimalString,
    stringToNumTwoDecimal
  }
}

export default NumberFormatter;