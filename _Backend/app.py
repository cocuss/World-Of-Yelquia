from flask import Flask, request, jsonify

app = Flask(__name__)


@app.route("/chat", methods=["POST"])
def chat():
    data = request.get_json()

    npc_id = data.get("npcId")
    player_id = data.get("playerId")
    message = data.get("message")

    print("----- NUEVO MENSAJE -----")
    print(f"NPC: {npc_id}")
    print(f"Player: {player_id}")
    print(f"Message: {message}")

    # Respuesta temporal.
    # Más adelante aquí conectaremos el modelo de IA.
    response = f"Hola, viajero. Recibí tu mensaje: {message}"

    return jsonify({
        "npcId": npc_id,
        "response": response
    })


if __name__ == "__main__":
    app.run(
        host="127.0.0.1",
        port=5000,
        debug=True
    )