namespace Tax.Simulator
{
    public class Situation
    {
        private readonly SituationFamiliale _situationFamiliale;
        private readonly decimal _salaireMensuel;
        private readonly decimal _salaireMensuelConjoint;
        private readonly int _nombreEnfant;

        public Situation(string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfant)
        {
            _situationFamiliale = situationFamiliale.ToSituationFamiliale();
            _salaireMensuel = salaireMensuel >= 0 ? salaireMensuel : throw new ArgumentException("Les salaires doivent être positifs.");
            _salaireMensuelConjoint = salaireMensuelConjoint >= 0 ? salaireMensuelConjoint : throw new ArgumentException("Les salaires doivent être positifs.");
            _nombreEnfant = nombreEnfant >= 0 ? nombreEnfant : throw new ArgumentException("Le nombre d'enfants ne peut pas être négatif.");
        }
    }
}
